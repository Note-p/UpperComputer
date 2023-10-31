using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ins.Forms.martix;

namespace Ins.Forms
{
    public class Calibration1
    {
       
        double[] x = new double[200 * 5000];
        double[] y = new double[200 * 5000];
        double[] z = new double[200 * 5000];


        public uint cursor = 0;
        public uint cursor_start = 500;

        public void Calibration(List<double> x, List<double> y, List<double> z, double[] center, Matrix trans_mat)
        {
            double refr = 1; // reference radius

            // Read data from file
            /* FILE* fp = fopen("NFData2023_3_22_20_8_59.txt", "r");
             if (fp == NULL) {
                 printf("Failed to open the file.\n");
                 return 1;
             }*/

            int start = 500;
            int numPoints = (int)(cursor - cursor_start);
            //double x[buffersize], y[buffersize], z[buffersize];

            // Skip lines until start position
            /*for (int i = 0; i < start; i++) {
                if (fscanf(fp, "%*f %*f %*f") == EOF) {
                    printf("Not enough data points.\n");
                    fclose(fp);
                    return 1;
                }
            }*/

            // Read data into arrays
            /*while (fscanf(fp, "%lf %lf %lf", &x[numPoints], &y[numPoints], &z[numPoints]) != EOF) {
                numPoints++;
            }
            fclose(fp);*/

            // Do the fitting
            double[] radii=new double[3];
            double[] v = new double[3];
            Matrix evecs = new Matrix();
            ellipsoid_fit(x, y, z, numPoints, center, radii, evecs);

            // Calculate g_mat and trans_mat
            
            double[] g_mat = new double[9];
            g_mat[0] = 1 / radii[0] * refr;
            g_mat[4] = 1 / radii[1] * refr;
            g_mat[8] = 1 / radii[2] * refr;

            Matrix g_mat_M = Matrix_gen(3, 3, g_mat);
            Matrix tmptrnas = M_mul(M_mul(evecs, g_mat_M), M_Inverse(evecs));
            trans_mat.column = tmptrnas.column;
            trans_mat.row = tmptrnas.row;
            trans_mat.data = tmptrnas.data;
        }

        void ellipsoid_fit(List<double> x, List<double> y, List<double> z, int N, double[] center, double[] radii, Matrix evecs)
        {
            // Ellipsoid fitting code goes here...

            double[] x2 = new double[N];
            double[] y2 = new double[N];
            double[] z2 = new double[N];

            for (int i = 0; i < N; i++)
            {
                x2[i] = x[i] * x[i];
                y2[i] = y[i] * y[i];
                z2[i] = z[i] * z[i];
            }

            double[] D = new double[9*N];
            for (int i = 0, j = 0; i < N * 9; i = i + 9, j++)
            {
                D[i] = x2[j] + y2[j] - 2 * z2[j];
                D[i + 1] = x2[j] - 2 * y2[j] + z2[j];
                D[i + 2] = 4 * x[j] * y[j];
                D[i + 3] = 2 * x[j] * z[j];
                D[i + 4] = 2 * y[j] * z[j];
                D[i + 5] = 2 * x[j];
                D[i + 6] = 2 * y[j];
                D[i + 7] = 2 * z[j];
                D[i + 8] = 1;
            }
            Matrix D_M = Matrix_gen(N, 9, D);

            double[] R = new double[N];
            for (int i = 0; i < N; i++)
            {
                R[i] = x2[i] + y2[i] + z2[i];
            }
            Matrix R_M = Matrix_gen(N, 1, R);

            Matrix tmp1 = M_mul(MT(D_M), D_M);
            Matrix tmp2 = M_mul(MT(D_M), R_M);
            Matrix b = M_mul(M_Inverse(tmp1), tmp2);

            double[] mtxref = new double[]
{
    3, 1, 1, 0, 0, 0, 0, 0, 0, 0,
    3, 1, -2, 0, 0, 0, 0, 0, 0, 0,
    3, -2, 1, 0, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 2, 0, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 1, 0, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 1, 0, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
    0, 0, 0, 0, 0, 0, 0, 1, 0, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 1, 0,
    0, 0, 0, 0, 0, 0, 0, 0, 0, 1
};

            Matrix mtxref_M = Matrix_gen(10, 10, mtxref);

            double[] tmp3 = new double[10];
            tmp3[0] = -1.0 / 3.0;
            Matrix tmp3_M = Matrix_gen(10, 1, tmp3);
            M_matFull(tmp3_M, 2, 1, b);

            Matrix v_M = M_mul(mtxref_M, tmp3_M);

            double nn = v_M.data[9];
            for (int i = 0; i < 9; i++)
            {
                v_M.data[i] = -v_M.data[i];
            }

            double[] v = v_M.data;
            double[] A = new double[]{
        v[0], v[3], v[4], v[6],
        v[3], v[1], v[5], v[7],
        v[4], v[5], v[2], v[8],
        v[6], v[7], v[8], -nn };
            Matrix A_M = Matrix_gen(4, 4, A);

            Matrix tmp4 = MNumul(MCut(A_M, 1, 3, 1, 3), -1);
            double[] tmp5 = new double[]{ v[6], v[7], v[8] };
            Matrix tmp5_M = Matrix_gen(3, 1, tmp5);
            Matrix ofs_M = M_mul(M_Inverse(tmp4), tmp5_M);//ofs=-A(1:3,1:3)\[v(7);v(8);v(9)]; % offset is center of ellipsoid

            Matrix Tmtx_M = M_I(4);
            M_matFull(Tmtx_M, 4, 1, MT(ofs_M));

            Matrix AT_M = M_mul(M_mul(Tmtx_M, A_M), MT(Tmtx_M));

            Matrix tmp6 = MCut(AT_M, 1, 3, 1, 3);
            Matrix tmp7 = MNumul(tmp6, -1.0 / AT_M.data[15]);
            Matrix[] eig_M = M_eigen(tmp7); //[rotM ev]=eig(AT(1:3,1:3)/-AT(4,4));



            double[] gain=new double[3];
            for (int i = 0; i < 3; i++)
            {
                gain[i] = Math.Sqrt(1.0 / eig_M[0].data[i]);
                radii[i] = gain[i];
            }

            // Set outputs
            for (int i = 0; i < ofs_M.column * ofs_M.row; i++)
            {
                center[i] = ofs_M.data[i];
            }
            evecs.column = eig_M[1].column;
            evecs.row = eig_M[1].row;
            evecs.data = eig_M[1].data;



        }

        public void polyfit(List<double> d_X, List<double> d_Y, int d_N, int rank, double[] coeff)
        {
            if (3 != rank)  //判断次数是否合法
                return;

            int i, j, k;
            double[,] aT_A = new double[4, 4]; 
            double[] aT_Y = new double[4];    


            for (i = 0; i < rank + 1; i++)  //行
            {
                for (j = 0; j < rank + 1; j++)  //列
                {
                    for (k = 0; k < d_N; k++)
                    {
                        aT_A[i,j] += Math.Pow(d_X[k], i + j);       //At * A 线性矩阵
                    }
                }
            }

            for (i = 0; i < rank + 1; i++)
            {
                for (k = 0; k < d_N; k++)
                {
                    aT_Y[i] += Math.Pow(d_X[k], i) * d_Y[k];     //At * Y 线性矩阵
                }
            }

            //以下为高斯列主元素消去法解线性方程组
            for (k = 0; k < rank + 1 - 1; k++)
            {
                int row = k;
                double mainElement = Math.Abs(aT_A[k,k]);
                double temp = 0.0;

                //找主元素
                for (i = k + 1; i < rank + 1 - 1; i++)
                {
                    if (Math.Abs(aT_A[i,i]) > mainElement)
                    {
                        mainElement = Math.Abs(aT_A[i,i]);
                        row = i;
                    }
                }

                //交换两行
                if (row != k)
                {
                    for (i = 0; i < rank + 1; i++)
                    {
                        temp = aT_A[k,i];
                        aT_A[k,i] = aT_A[row,i];
                        aT_A[row,i] = temp;
                    }
                    temp = aT_Y[k];
                    aT_Y[k] = aT_Y[row];
                    aT_Y[row] = temp;
                }


                //消元过程
                for (i = k + 1; i < rank + 1; i++)
                {
                    for (j = k + 1; j < rank + 1; j++)
                    {
                        aT_A[i,j] -= aT_A[k,j] * aT_A[i,k] / aT_A[k,k];
                    }
                    aT_Y[i] -= aT_Y[k] * aT_A[i,k] / aT_A[k,k];
                }
            }

            //回代过程
            for (i = rank + 1 - 1; i >= 0; coeff[i] /= aT_A[i,i], i--)
            {
                for (j = i + 1, coeff[i] = aT_Y[i]; j < rank + 1; j++)
                {
                    coeff[i] -= aT_A[i,j] * coeff[j];
                }
            }

            return;
        }
    }
}
