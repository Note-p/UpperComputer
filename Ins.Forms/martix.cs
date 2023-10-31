using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ins.Forms
{

    public class martix
    {
        public class Matrix
        {
            // Store Matrix
            // 存储矩阵
            public int row;
            public int column;
            public double[] data;
           
        }

        public class ElementaryTransformation
        {
            // Store the Operation of Elementary_Transformation
            // 存储初等变化的运算过程
            public int minuend_line;
            public int subtractor_line;
            public double scale;
            public ElementaryTransformation forward_E_trans;
            public ElementaryTransformation next_E_trans;
        }

        public class UpperTriangularTransformation
        {
            // Store the result of Upper_triangular_transformation
            // 存储上三角化的运算结果
            public Matrix trans_matrix;
            public Matrix Uptri_matrix;
        }

        public class LowerTriangularTransformation
        {
            // Store the result of Lower_triangular_transformation
            // 存储下三角化的运算结果
            public Matrix trans_matrix;
            public Matrix Lowtri_matrix;
        }

        public class DiagonalizationTransformation
        {
            // Store the result of Diagonalization_transformation
            // 存储对角化化的运算结果
            public Matrix trans_leftmatrix;
            public Matrix Diatri_matrix;
            public Matrix trans_rightmatrix;
        }

        public class MatrixInverseStruct
        {
            // Store the result of matrix_inverse
            // 存储求逆运算的中间结果，提高算法效率
            public Matrix _matrix;
            public ElementaryTransformation _Etrans_head;
        }

        public class MatrixEigenStructSingle
        {
            // Store the result of matrix_eigen
            // 存储求最大特征值运算的结果
            public Matrix eigen_matrix;
            public double eigen_value;
        }

        public  double M_cond(Matrix _mat, int Setting)
        {
            double matrix_cond = 0;

            if (_mat.column == _mat.row)
            {
                if (Setting == 1 || Setting == 2 || Setting == int.MaxValue || Setting == int.MinValue)
                {
                    Matrix _mat_inv = M_Inverse(_mat);
                    matrix_cond = M_norm(_mat, Setting) * M_norm(_mat_inv, Setting);
                }
                else
                {
                    Console.WriteLine("M_cond_025"); // Replace with the actual message
                    Console.ReadLine(); // Use Console.ReadLine() instead of system("pause") in C#
                }
            }
            else
            {
                Console.WriteLine("M_cond_024"); // Replace with the actual message
                Console.ReadLine(); // Use Console.ReadLine() instead of system("pause") in C#
            }

            return matrix_cond;
        }

        public static Matrix Hilbert(int order)
        {
            Matrix HilbertMat = new Matrix
            {
                row = order,
                column = order,
                data = new double[order* order]
            };

            for (int i = 0; i < order; i++)
            {
                for (int j = 0; j < order; j++)
                {
                    if (i > j)
                    {
                        HilbertMat.data[i * order + j] = HilbertMat.data[j * order + i];
                    }
                    else
                    {
                        HilbertMat.data[i * order + j] = 1.0 / (i + j + 1);
                    }
                }
            }

            return HilbertMat;
        }

        public static void EtransFree(ElementaryTransformation etrans)
        {
            ElementaryTransformation tempEtrans = etrans;
            do
            {
                tempEtrans = tempEtrans.forward_E_trans;
                if (tempEtrans != null)
                {
                    tempEtrans.next_E_trans = null;
                }
            } while (tempEtrans != null);
        }

        public static int M_rank(Matrix mat)
        {
            Console.WriteLine("Matrix Rank: " + mat.row);

            int rank = 0;
            int row = mat.row;
            int column = mat.column;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (mat.data[i * column + j] != 0)
                    {
                        rank++;
                        break;
                    }
                }
            }

            return rank;
        }

        public static void help(string fileName)
        {
            Console.WriteLine(">>HELP(" + fileName + ")");
            string tempRoute = "./help/" + fileName + ".txt";

            try
            {
                string content = System.IO.File.ReadAllText(tempRoute);
                Console.WriteLine(content);
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine(fileName + " not found!");
            }
        }

        public static Matrix Matrix_gen(int row, int column, double[] data)
        {
            Matrix _mat = new Matrix();
            _mat.row = row;
            _mat.column = column;
            int size =  _mat.row * _mat.column;
            _mat.data = new double[size];
            int i;
            for (i = 0; i < size; i++)
            {
                _mat.data[i] = data[i];
            }
            return _mat;
        }

        public static Matrix MatrixCopy(Matrix sourceMatrix)
        {
            // 复制矩阵（生成新矩阵）
            Matrix _matCopy = Matrix_gen(sourceMatrix.row, sourceMatrix.column, sourceMatrix.data);
            return _matCopy;
        }

        public static Matrix M_add_sub(double scaleMatSubed, Matrix matSubed, double scaleMatMinus, Matrix matMinus)
        {
            // 矩阵加减法
            Matrix matResult = null;
            if (matSubed.column == matMinus.column && matSubed.row == matMinus.row)
            {
                matResult = MatrixCopy(matSubed);
                int size = matSubed.row * matSubed.column;
                for (int i = 0; i < size; i++)
                {
                    matResult.data[i] = (matResult.data[i] * scaleMatSubed) - (matMinus.data[i] * scaleMatMinus);
                }
            }
            else
            {
                Console.WriteLine("M_add_sub_003"); // 输出错误消息
            }
            return matResult;
        }

        public static Matrix M_mul(Matrix _mat_left, Matrix _mat_right)
        {           
            /*Determine_Matrix_Dimensions*/
            Matrix _mat_result = null;
            if (_mat_left.column != _mat_right.row)
            {
                Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_mul_001)_mat_left->column != _mat_right->row\n");
            }
            else
            {
                _mat_result = new Matrix();
                int row = _mat_left.row;
                int mid = _mat_left.column;
                int column = _mat_right.column;
                int i, j, k;
                double[] _data = new double[row * column];
                double temp = 0;
                /*Ergodic*/
                for (i = 0; i < row; i++)
                {
                    for (j = 0; j < column; j++)
                    {
                        /*Caculate Element*/
                        temp = 0;
                        for (k = 0; k < mid; k++)
                        {
                            temp += (_mat_left.data[i * mid + k]) * (_mat_right.data[k * column + j]);
                        }
                        _data[i * column + j] = temp;
                    }
                }
                _mat_result.row = row;
                _mat_result.column = column;
                _mat_result.data = _data;
            }

            return _mat_result;
        }

        public static int MatrixPrint(Matrix mat)
        {
            Console.WriteLine($">>Matrix_{mat}:");

            for (int i = 0; i < mat.row; i++)
            {
                for (int j = 0; j < mat.column; j++)
                {
                    Console.Write("%.6lf\t", mat.data[i * mat.column + j]);
                }
                Console.WriteLine();
            }

            return 0;
        }

        public static Matrix M_I(int order)
        {
            Matrix IMat = new Matrix();
            IMat.column = order;
            IMat.row = order;
            int size = order * order;
            IMat.data=new double[size];
            for (int i = 0; i < size; i++)
            {
                IMat.data[i] = 0;
            }

            for (int i = 0; i < order; i++)
            {
                IMat.data[i * (order + 1)] = 1;
            }

            return IMat;
        }


        public static int M_E_trans(Matrix _mat,  ElementaryTransformation _Etrans_, int line_setting)
        {
            int line_num, i;
            if (line_setting != 0)
            {
                /*行初等变换*/
                line_num = _mat.column;
                if (_Etrans_.scale!=0)
                {
                    for (i = 0; i < line_num; i++)
                    {
                        _mat.data[(_Etrans_.minuend_line - 1) * (_mat.column) + i] -=
                            (_Etrans_.scale) * (_mat.data[(_Etrans_.subtractor_line - 1) * (_mat.column) + i]);

                    }
                }
                else
                {
                    if ((_Etrans_.minuend_line < 0) && (_Etrans_.subtractor_line < 0))
                    {/*交换*/
                        MSwap(_mat, -(_Etrans_.minuend_line), -(_Etrans_.subtractor_line), line_setting);
                    }
                }
            }
            else
            {
                /*列初等变换*/
                line_num = _mat.row;
                if (_Etrans_.scale!=0)
                {
                    for (i = 0; i < line_num; i++)
                    {
                        _mat.data[(_Etrans_.minuend_line - 1) + (_mat.column) * i] -=
                            (_Etrans_.scale) * (_mat.data[(_Etrans_.subtractor_line - 1) + (_mat.column) * i]);
                    }
                }
                else
                {
                    if ((_Etrans_.minuend_line < 0) && (_Etrans_.subtractor_line < 0))
                    {/*交换*/
                        MSwap(_mat, -(_Etrans_.minuend_line), -(_Etrans_.subtractor_line), line_setting);
                    }
                }
            }
            return 0;
        }

        public static Matrix Etrans_4_Inverse(Matrix _mat_result, ElementaryTransformation _Etrans_, int line_setting)
        {
            ElementaryTransformation temp_Etrans = _Etrans_, temp_Etrans_pre = _Etrans_;
            int temp_num = 0;
            // 此处方案感谢 @1u2e, github.com/Amoiensis/Matrix_hub/issues/4
            while (temp_Etrans != null)
            {
                temp_num = temp_Etrans.minuend_line;
                temp_Etrans.minuend_line = temp_Etrans.subtractor_line;
                temp_Etrans.subtractor_line = temp_num;
                M_E_trans(_mat_result, temp_Etrans, line_setting);
                // 此处修改方案感谢 @1u2e, github.com/Amoiensis/Matrix_hub/issues/4
                temp_Etrans = temp_Etrans.forward_E_trans;
               
                temp_Etrans_pre = temp_Etrans;
            }
            return _mat_result;
        }

        public  Matrix Etrans_2_Matrix(ElementaryTransformation _Etrans_, int order, int line_setting)
        {
            ElementaryTransformation temp_Etrans = _Etrans_;
            Matrix _mat_result = M_I(order);

            if (_Etrans_ != null)
            {
                while (temp_Etrans.next_E_trans != null)
                {
                    temp_Etrans = temp_Etrans.next_E_trans;
                }

                do
                {
                    temp_Etrans.scale = -temp_Etrans.scale;
                    M_E_trans(_mat_result,  temp_Etrans, line_setting);
                    temp_Etrans.scale = -temp_Etrans.scale;
                    temp_Etrans = temp_Etrans.forward_E_trans;

                    if (temp_Etrans != null)
                    {
                        temp_Etrans.next_E_trans = null;
                    }
                } while (temp_Etrans != null);
            }

            return _mat_result;
        }

        public UpperTriangularTransformation MUptri(Matrix matSource)
        {
            Matrix mat = MatrixCopy(matSource);
            int i, j, k, flag;
            ElementaryTransformation Etrans_temp_last = null;
            ElementaryTransformation Etrans_temp_head = null;

            for (i = 0; i < mat.column; i++)
            {
                for (j = i + 1; j < mat.row; j++)
                {
                    flag = 0;
                    ElementaryTransformation Etrans_temp = new ElementaryTransformation();
                    Etrans_temp.minuend_line = j + 1;
                    Etrans_temp.subtractor_line = i + 1;
                    if (mat.data[i * mat.column + i] != 0)
                    {
                        Etrans_temp.scale = mat.data[j * mat.column + i] / mat.data[i * mat.column + i];
                    }
                    else
                    {
                        Etrans_temp.scale = 0;
                        for (k = i + 1; k < mat.row; k++)
                        {
                            flag = 1;
                            if (mat.data[k * mat.column + i] != 0)
                            {
                                Etrans_temp.minuend_line = -(i + 1);
                                Etrans_temp.subtractor_line = -(k + 1);
                                flag = 2;
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            break;
                        }
                    }
                    Etrans_temp.forward_E_trans = null;
                    Etrans_temp.next_E_trans = null;
                    if (Etrans_temp_head == null)
                    {
                        Etrans_temp_head = Etrans_temp;
                        Etrans_temp.forward_E_trans = null;
                    }
                    else
                    {
                        Etrans_temp.forward_E_trans = Etrans_temp_last;
                    }
                    if (i + 1 == mat.column)
                    {
                        Etrans_temp.next_E_trans = null;
                    }
                    else
                    {
                        if (Etrans_temp_last != null)
                        {
                            Etrans_temp_last.next_E_trans = Etrans_temp;
                        }
                    }
                    M_E_trans(mat, Etrans_temp, 1);
                    // M_print(mat); //显示具体矩阵
                    Etrans_temp_last = Etrans_temp;

                    if (flag == 2)
                    {
                        i = i - 1;
                        break;
                    }
                }
            }
            Matrix transMat = Etrans_2_Matrix(Etrans_temp_head, mat.row, 1);
            UpperTriangularTransformation Uptri = new UpperTriangularTransformation();
            Uptri.trans_matrix = transMat;
            Uptri.Uptri_matrix = mat;

  

            return Uptri;
        }


        public static MatrixInverseStruct MUptri4Inv(Matrix matSource)
        {
            Matrix _mat = MatrixCopy(matSource);
            int i, j, k, flag;
            ElementaryTransformation _Etrans_temp_last = null;
            ElementaryTransformation _Etrans_temp_head = null;

            /*初等变换*/
            for (i = 0; i < _mat.column; i++)
            {
                for (j = i + 1; j < _mat.row; j++)
                {
                    flag = 0;
                    ElementaryTransformation _Etrans_temp = new ElementaryTransformation();
                    _Etrans_temp.minuend_line = j + 1;
                    _Etrans_temp.subtractor_line = i + 1;
                    if ((_mat.data[(_mat.column) * i + i]) != 0)
                    {
                        _Etrans_temp.scale = (_mat.data[(_mat.column) * j + i]) / (_mat.data[(_mat.column) * i + i]);
                    }
                    else
                    {
                        _Etrans_temp.scale = 0;
                        for (k = i + 1; k < _mat.row; k++)
                        {
                            flag = 1;//无可替代行
                            if ((_mat.data[(_mat.column) * k + i]) != 0)
                            {
                                _Etrans_temp.minuend_line = -(i + 1);
                                _Etrans_temp.subtractor_line = -(k + 1);
                                flag = 2;//表示能够替换行
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            break;
                        }
                    }
                    _Etrans_temp.forward_E_trans = null;
                    _Etrans_temp.next_E_trans = null;
                    //if (j==1){
                    if (_Etrans_temp_head == null)
                    {
                        _Etrans_temp_head = _Etrans_temp;
                        _Etrans_temp.forward_E_trans = null;
                    }
                    else
                    {
                        _Etrans_temp.forward_E_trans = _Etrans_temp_last;

                    }
                    if ((i + 1) == _mat.column)
                    {
                        _Etrans_temp.next_E_trans = null;
                    }
                    else
                    {
                        if (_Etrans_temp_last != null)
                        {
                            _Etrans_temp_last.next_E_trans = _Etrans_temp;
                        }
                    }
                    M_E_trans(_mat, _Etrans_temp, 1);
                    //M_print(_mat); //显示具体矩阵
                    _Etrans_temp_last = _Etrans_temp;

                    if (flag == 2)
                    {
                        i = i - 1;
                        break;
                    }
                }
            }
            MatrixInverseStruct _Uptri = new MatrixInverseStruct();
            _Uptri._matrix = _mat;
            _Uptri._Etrans_head = _Etrans_temp_last;
            return _Uptri;
        }




        LowerTriangularTransformation M_Lowtri_(Matrix _mat_source)
        {
            Matrix _mat = MatrixCopy(_mat_source);
            int i, j, k, flag;
            ElementaryTransformation _Etrans_temp_last = null;
            ElementaryTransformation _Etrans_temp_head = null;
            for (i = 0; i < _mat.row; i++)
            {
                for (j = i + 1; j < _mat.column; j++)
                {
                    flag = 0;
                    ElementaryTransformation _Etrans_temp = new ElementaryTransformation();
                    _Etrans_temp.minuend_line = j + 1;
                    _Etrans_temp.subtractor_line = i + 1;

                    if (_mat.data[i * _mat.column + i] != 0)
                    {
                        _Etrans_temp.scale = _mat.data[i * _mat.column + j] / _mat.data[i * _mat.column + i];
                    }
                    else
                    {
                        _Etrans_temp.scale = 0;
                        for (k = i + 1; k < _mat.row; k++)
                        {
                            flag = 1; //无可替代行
                            if (_mat.data[k * _mat.column + i] != 0)
                            {
                                _Etrans_temp.minuend_line = -(i + 1);
                                _Etrans_temp.subtractor_line = -(k + 1);
                                flag = 2; //表示能够替换行
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            break;
                        }
                    }

                    _Etrans_temp.forward_E_trans = null;
                    _Etrans_temp.next_E_trans = null;
                    if (_Etrans_temp_head == null)
                    {
                        _Etrans_temp_head = _Etrans_temp;
                        _Etrans_temp.forward_E_trans = null;
                    }
                    else
                    {
                        _Etrans_temp.forward_E_trans = _Etrans_temp_last;
                    }
                    if (i + 1 == _mat.column)
                    {
                        _Etrans_temp.next_E_trans = null;
                    }
                    else
                    {
                        if (_Etrans_temp_last != null)
                        {
                            _Etrans_temp_last.next_E_trans = _Etrans_temp;
                        }
                    }
                    M_E_trans(_mat, _Etrans_temp, 0);
                    _Etrans_temp_last = _Etrans_temp;
                    if (flag == 2)
                    {
                        i = i - 1;
                        break;
                    }
                }
            }
            Matrix trans_mat = Etrans_2_Matrix(_Etrans_temp_head, _mat.row, 0);
            LowerTriangularTransformation _Lowtri = new LowerTriangularTransformation();
            _Lowtri.trans_matrix = trans_mat;
            _Lowtri.Lowtri_matrix = _mat;
            Console.WriteLine(">>Lowtri(Matrix_{0})=", _mat_source);
            Console.WriteLine("\tMatrix_{0} * Matrix_{1}", _mat, trans_mat);
            return _Lowtri;
        }

        public static MatrixInverseStruct M_Lowtri_4inv(Matrix _mat_source)
        {
            Matrix _mat = MatrixCopy(_mat_source);
            int i, j, k, flag;
            ElementaryTransformation _Etrans_temp_last = null;
            ElementaryTransformation _Etrans_temp_head = null;
            for (i = 0; i < _mat.row; i++)
            {
                for (j = i + 1; j < _mat.column; j++)
                {
                    flag = 0;
                    ElementaryTransformation _Etrans_temp = new ElementaryTransformation();
                    _Etrans_temp.minuend_line = j + 1;
                    _Etrans_temp.subtractor_line = i + 1;


                    if ((_mat.data[(_mat.column) * i + i]) != 0)
                    {
                        _Etrans_temp.scale = (_mat.data[(_mat.column) * i + j]) / (_mat.data[(_mat.column) * i + i]); ;
                    }
                    else
                    {
                        _Etrans_temp.scale = 0;
                        for (k = i + 1; k < _mat.row; k++)
                        {
                            flag = 1;//无可替代行
                            if ((_mat.data[(_mat.column) * k + i]) != 0)
                            {
                                _Etrans_temp.minuend_line = -(i + 1);
                                _Etrans_temp.subtractor_line = -(k + 1);
                                flag = 2;//表示能够替换行
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            break;
                        }
                    }

                    _Etrans_temp.forward_E_trans = null;
                    _Etrans_temp.next_E_trans = null;
                    if (_Etrans_temp_head == null)
                    {
                        _Etrans_temp_head = _Etrans_temp;
                        _Etrans_temp.forward_E_trans = null;
                    }
                    else
                    {
                        _Etrans_temp.forward_E_trans = _Etrans_temp_last;
                    }
                    if ((i + 1) == _mat.column)
                    {
                        _Etrans_temp.next_E_trans = null;
                    }
                    else
                    {
                        if (_Etrans_temp_last != null)
                        {
                            _Etrans_temp_last.next_E_trans = _Etrans_temp;
                        }
                    }
                    M_E_trans(_mat, _Etrans_temp, 0);
                    //M_print(_mat); //显示具体矩阵
                    _Etrans_temp_last = _Etrans_temp;
                    if (flag == 2)
                    {
                        i = i - 1;
                        break;
                    }
                }
            }
            MatrixInverseStruct _Lowtri = new MatrixInverseStruct();
            _Lowtri._matrix = _mat;
            _Lowtri._Etrans_head = _Etrans_temp_last;
            return _Lowtri;
        }

        public static Matrix M_Dia_Inv(Matrix _mat_source)
        {
            Matrix _mat_inv = null;
            if (_mat_source.row != _mat_source.column)
            {
                Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_Dia_Inv_002)_mat_left->column != _mat_right->row\n");
                // You can replace the "system("pause")" with a suitable way to pause the program in C#.
            }
            else
            {
                _mat_inv = MatrixCopy(_mat_source);
                double[] data = _mat_inv.data;
                int i, order = _mat_source.column;
                for (i = 0; i < order; i++)
                {
                    if (data[i * (order + 1)] == 0)
                    { // 不可逆
                        Console.WriteLine("@ERROR: Matrix is not invertible!\n\t Details:(M_Dia_Inv_023)Please Check: Inverse element of Dia == 0! \n");
                        // You can replace the "system("pause")" with a suitable way to pause the program in C#.
                        data[i * (order + 1)] = 1 / data[i * (order + 1)];
                    }
                    else
                    {
                        data[i * (order + 1)] = 1 / data[i * (order + 1)];
                    }
                }
            }
            return _mat_inv;
        }

        public DiagonalizationTransformation MDiatri(Matrix matSource)
        {
            DiagonalizationTransformation Dia_instance = new DiagonalizationTransformation();
            UpperTriangularTransformation Uptri_ = MUptri(matSource);
            LowerTriangularTransformation Lowtri_ = M_Lowtri_(Uptri_.Uptri_matrix);
            Dia_instance.trans_leftmatrix = Uptri_.trans_matrix;
            Dia_instance.Diatri_matrix = Lowtri_.Lowtri_matrix;
            Dia_instance.trans_rightmatrix = Lowtri_.trans_matrix;

            return Dia_instance;
        }

        public static Matrix M_Inverse(Matrix _mat)
        {
            MatrixInverseStruct _Uptri_ = MUptri4Inv(_mat);
            MatrixInverseStruct _Lowtri_ = M_Lowtri_4inv(_Uptri_._matrix);
            Matrix _mat_dia_inv = M_Dia_Inv(_Lowtri_._matrix);
            Matrix _mat_inv = Etrans_4_Inverse(_mat_dia_inv, _Lowtri_._Etrans_head, 1);
            _mat_inv = Etrans_4_Inverse(_mat_inv, _Uptri_._Etrans_head, 0);

            //Console.WriteLine(">>Inv(Matrix_{0})=", _mat);
            //Console.WriteLine("\tMatrix_inv_{0}", _mat_inv);


            return _mat_inv;
        }

        public static int MSwap(Matrix mat, int line1, int line2, int lineSetting)
        {
            line1 = line1 - 1;
            line2 = line2 - 1;
            int i;
            double temp;

            if (lineSetting == 1)
            {
                if (line1 < mat.row && line2 < mat.row)
                {
                    for (i = 0; i < mat.column; i++)
                    {
                        temp = mat.data[line1 * mat.column + i];
                        mat.data[line1 * mat.column + i] = mat.data[line2 * mat.column + i];
                        mat.data[line2 * mat.column + i] = temp;
                    }
                }
                else
                {
                    Console.WriteLine("@ERROR: Matrix_Swap_Line Over!\n\tDetails:(M_swap_004)_Swap_line over the limited\n");
                    // 处理错误，例如抛出异常或返回错误代码
                }
            }
            else
            {
                if (line1 < mat.column && line2 < mat.column)
                {
                    for (i = 0; i < mat.row; i++)
                    {
                        temp = mat.data[line1 + mat.column * i];
                        mat.data[line1 + mat.column * i] = mat.data[line2 + mat.column * i];
                        mat.data[line2 + mat.column * i] = temp;
                    }
                }
                else
                {
                    Console.WriteLine("@ERROR: Matrix_Swap_Line Over!\n\tDetails:(M_swap_004)_Swap_line over the limited\n");
                    // 处理错误，例如抛出异常或返回错误代码
                }
            }

            return 0;
        }


        public static Matrix MCut(Matrix mat, int rowHead, int rowTail, int columnHead, int columnTail)
        {
            Matrix matResult = null;

            if (rowTail < 0)
            {
                if (rowTail == -1)
                {
                    rowTail = mat.row;
                }
                else
                {
                    Console.WriteLine("@ERROR: Matrix_Cut Wrong!\n\tDetails:(M_Cut_007)_Range_can't_be_negative!'\n");
                    // 处理错误，例如抛出异常或返回错误代码
                }
            }

            if (rowHead < 0)
            {
                if (rowHead == -1)
                {
                    rowHead = mat.row;
                }
                else
                {
                    Console.WriteLine("@ERROR: Matrix_Cut Wrong!\n\tDetails:(M_Cut_007)_Range_can't_be_negative!'\n");
                    // 处理错误，例如抛出异常或返回错误代码
                }
            }

            if (columnTail < 0)
            {
                if (columnTail == -1)
                {
                    columnTail = mat.column;
                }
                else
                {
                    Console.WriteLine("@ERROR: Matrix_Cut Wrong!\n\tDetails:(M_Cut_007)_Range_can't_be_negative!'\n");
                    // 处理错误，例如抛出异常或返回错误代码
                }
            }

            if (columnHead < 0)
            {
                if (columnHead == -1)
                {
                    columnHead = mat.column;
                }
                else
                {
                    Console.WriteLine("@ERROR: Matrix_Cut Wrong!\n\tDetails:(M_Cut_007)_Range_can't_be_negative!'\n");
                    // 处理错误，例如抛出异常或返回错误代码
                }
            }

            if (rowTail > mat.row || columnTail > mat.column)
            {
                Console.WriteLine("@ERROR: Matrix_Cut Over!\n\tDetails:(M_Cut_005)_Cut_tail over_the limited\n");
                // 处理错误，例如抛出异常或返回错误代码
            }
            else
            {
                if (rowHead > rowTail || columnHead > columnTail)
                {
                    Console.WriteLine("@ERROR: Matrix_Cut Wrong!\n\tDetails:(M_Cut_006)_Head_>_Tail\n");
                    // 处理错误，例如抛出异常或返回错误代码
                }
                else
                {
                    rowHead = rowHead - 1;
                    columnHead = columnHead - 1;
                    matResult = new Matrix();
                    matResult.row = rowTail - rowHead;
                    matResult.column = columnTail - columnHead;
                    matResult.data = new double[matResult.row * matResult.column];

                    for (int i = 0; i < (rowTail - rowHead); i++)
                    {
                        for (int j = 0; j < (columnTail - columnHead); j++)
                        {
                            matResult.data[i * matResult.column + j] = mat.data[(i + rowHead) * mat.column + (j + columnHead)];
                        }
                    }
                }
            }

            return matResult;
        }

        public static Matrix MT(Matrix matSource)
        {
            Matrix mat = new Matrix(); // 创建一个新的矩阵对象，行列互换
            mat.row = matSource.column;
            mat.column = matSource.row;
            mat.data = new double[mat.row * mat.column];
            for (int i = 0; i < mat.row; i++)
            {
                for (int j = 0; j < mat.column; j++)
                {
                    mat.data[i * (mat.column) + j] = matSource.data[j * (matSource.column) + i];
                }
            }
            return mat;
        }
        public double MTr(Matrix mat)
        {
            double trMat = 0;
            if (mat.column == mat.row)
            {
                for (int i = 0; i < mat.column; i++)
                {
                    trMat += mat.data[i * (mat.column + 1)];
                }
            }
            else
            {
                Console.WriteLine("@ERROR: Matrix_trace Wrong!\n\tDetails:(M_tr_008)_ROW_!=_COLUMN.'\n");
                // 处理错误，例如抛出异常或返回错误代码
            }
            return trMat;
        }
        public double MDet(Matrix mat)
        {
            double detMat = 0;
            if (mat.column == mat.row)
            {
                UpperTriangularTransformation uptri = MUptri(mat); // 假设有一个名为M_Uptri的函数来获取上三角矩阵和变换矩阵
                Matrix uptriMatrix = uptri.Uptri_matrix;
                detMat = 1;
                for (int i = 0; i < uptriMatrix.column; i++)
                {
                    detMat *= mat.data[i * (mat.column + 1)];
                }

            }
            else
            {
                Console.WriteLine("@ERROR: Matrix_Determinant_ Wrong!\n\tDetails:(M_det_009)_ROW_!=_COLUMN.'\n");
                // 处理错误，例如抛出异常或返回错误代码
            }
            return detMat;
        }

        public Matrix MFull(Matrix mat, int rowUp, int rowDown, int columnLeft, int columnRight, double fullData)
        {
            Matrix matResult = new Matrix();
            matResult.row = mat.row + rowUp + rowDown;
            matResult.column = mat.column + columnLeft + columnRight;

            for (int i = 0; i < matResult.row; i++)
            {
                for (int j = 0; j < matResult.column; j++)
                {
                    if (i >= rowUp && i < (rowUp + mat.row))
                    {
                        if (j >= columnLeft && j < (columnLeft + mat.column))
                        {
                            matResult.data[i * (matResult.column) + j] = mat.data[(mat.column) * (i - rowUp) +
                       (j - columnLeft)];
                        }
                        else
                        {
                            matResult.data[i * (matResult.column) + j] = fullData;
                        }
                    }
                    else
                    {
                        matResult.data[i * (matResult.column) + j] = fullData;
                    }
                }
            }

            return matResult;
        }

        public static double M_norm(Matrix _mat, int Setting)
        {/*Caculate Matrix norm-num
    向量/矩阵范数 Setting=1 - 1范数，Setting=2 - 2范数*/
            double[] data = _mat.data;
            int row = _mat.row;
            int column = _mat.column;
            double Val_norm = 0;
            int i, j;
            if (row == 1 || column == 1)
            {/*向量的范数*/
                switch (Setting)
                {
                    case 1:
                        {/*向量的1范数*/
                            for (i = 0; i < row; i++)
                            {
                                for (j = 0; j < column; j++)
                                {
                                    /*使用abs()会提示，error C2668: “abs”: 对重载函数的调用不明确
                                    转而使用fabs().*/
                                    Val_norm += Math.Abs(data[i * (column) + j]);
                                }
                            }
                            break;
                        }
                    case 2:
                        {/*向量的2范数*/
                            for (i = 0; i < row; i++)
                            {
                                for (j = 0; j < column; j++)
                                {
                                    Val_norm += data[i * (column) + j] * data[i * (column) + j];
                                }
                            }
                            Val_norm = Math.Pow(Val_norm, 0.5);
                            break;
                        }
                    case int.MaxValue:
                        {/*向量的∞(inf)无穷范数*/
                            Matrix M_temp_0, M_temp_1;
                            M_temp_0 = M_abs(_mat);
                            M_temp_1 = M_max(M_temp_0);
                            int temp_num = (int)M_temp_1.data[0];
                            Val_norm = (M_temp_0).data[temp_num];
                          
                            break;
                        }
                    default:
                        {/*向量的p范数*/
                            for (i = 0; i < row; i++)
                            {
                                for (j = 0; j < column; j++)
                                {
                                    Val_norm += Math.Pow(data[i * (column) + j], Setting);
                                }
                            }
                            if (Val_norm < 0)
                            {
                                Console.WriteLine("@WARNING: ||A||_p = sum((a_ij)^p)^(1/p)\n\tFor matrix's p-normvalue, the result can not be a complex number! (e.g. A+Bi)\n");
                            }
                            Val_norm = Math.Pow(Val_norm, 1.0 / Setting);
                            break;
                        }
                }
            }
            else
            {
                /*矩阵范数*/
                switch (Setting)
                {
                    case 1:
                        {/*矩阵的1范数*/
                            Matrix M_temp_0, M_temp_1, M_temp_2;
                            M_temp_0 = M_abs(_mat);
                            M_temp_1 = M_sum(M_temp_0);
                            M_temp_2 = M_max(M_temp_1);
                            int temp_num = (int)M_temp_2.data[0];
                            Val_norm = (M_temp_1).data[temp_num];
                           
                            break;
                        }
                    case 2:
                        {/*矩阵的2范数*/
                            Matrix M_temp_0, M_temp_1;
                            M_temp_0 = MT(_mat);
                            M_temp_1 = M_mul(M_temp_0, _mat);
                            MatrixEigenStructSingle M_temp_1_eigen = M_eigen_max(M_temp_1);
                            Val_norm = M_temp_1_eigen.eigen_value;
                            break;
                        }
                    case int.MaxValue:
                        {/*矩阵的∞(inf)无穷范数*/
                            Matrix M_temp_0, M_temp_1, M_temp_2, M_temp_;
                            M_temp_ = MT(_mat);
                            MatrixPrint(M_temp_);
                            M_temp_0 = M_abs(M_temp_);
                            MatrixPrint(M_temp_0);
                            M_temp_1 = M_sum(M_temp_0);
                            MatrixPrint(M_temp_1);
                            M_temp_2 = M_max(M_temp_1);
                            MatrixPrint(M_temp_2);
                            int temp_num = (int)M_temp_2.data[0];
                            Val_norm = (M_temp_1).data[temp_num];

                            break;
                        }
                    case int.MinValue:
                        {/*矩阵的F范数（Frobenius范数）*/
                            for (i = 0; i < row; i++)
                            {
                                for (j = 0; j < column; j++)
                                {
                                    Val_norm += data[i * (column) + j] * data[i * (column) + j];
                                }
                            }
                            Val_norm = Math.Pow(Val_norm, 0.5);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("@ERROR: M_norm Wrong!\n\t Details:(M_norm_022)The Norm-Setting should be 1/2/INF/p for Vector and 1/2/INF/FRO for Matrix!\n");
                            break;
                        }
                }
            }
            return Val_norm;
        }
        public static Matrix M_abs(Matrix _mat_origin)
        {/*Matrix Taking Absolute Value
    矩阵所有元素取绝对值*/
            Matrix _mat = new Matrix();
            _mat.row = _mat_origin.row;
            _mat.column = _mat_origin.column;
            int size = _mat.row * _mat.column;
            _mat.data = new double[size];
            int i;
            for (i = 0; i < size; i++)
            {
                _mat.data[i] = Math.Abs(_mat_origin.data[i]);
            }

            return _mat;
        }
        public static Matrix MNumul(Matrix mat, double num)
        {
            double[] data = mat.data;
            int Size_mat = (mat.row) * (mat.column), i;
            for (i = 0; i < Size_mat; i++)
            {
                data[i] = data[i] * num;
            }
            return mat;
        }

        public static Matrix M_matFull(Matrix _mat, int row_up, int column_left, Matrix _mat_full)
        {/*Full
    使用矩阵填充矩阵
    [注] 最左侧，和最上侧，row_up和column_left取值为_HEAD_(1)*/
            double[] data = _mat.data;
            row_up--;
            column_left--;
            int row = _mat.row;
            int column = _mat.column;
            int i, j;
            if ((row_up + _mat_full.row) > row)
            {
                Console.WriteLine("@ERROR: M_matFull Wrong!\n\tDetails:(M_Mfull_010)_ROW_OVER!.'\n");
                return _mat;
            }
            if ((column_left + _mat_full.column) > column)
            {
                Console.WriteLine("@ERROR: M_matFull Wrong!\n\tDetails:(M_Mfull_011)_COLUMN_OVER!.'\n");
                return _mat;
            }
            int full_row = _mat_full.row, full_column = _mat_full.column;
            for (i = 0; i < full_row; i++)
            {
                for (j = 0; j < full_column; j++)
                {
                    data[(row_up + i) * column + (column_left + j)] = _mat_full.data[i * full_column + j];
                }
            }
            return _mat;
        }

        public static Matrix M_Zeros(int row, int column)
        {/*Generate Zeros _matrix
    生成全零矩阵*/
            
            Matrix Zero_mat = new Matrix();
            Zero_mat.column = column;
            Zero_mat.row = row;
            int size = row * column, i;
            Zero_mat.data = new double[size];
            for (i = 0; i < size; i++)
            {
                Zero_mat.data[i] = 0;
            }
            return Zero_mat;
        }

        public static Matrix M_Ones(int row, int column)
        {/*Generate Ones _matrix
    生成全一矩阵*/
            Matrix Zero_mat = new Matrix();
            Zero_mat.column = column;
            Zero_mat.row = row;
            int size = row * column, i;
            Zero_mat.data = new double[size];
            for (i = 0; i < size; i++)
            {
                Zero_mat.data[i] = 0;
            }
            return Zero_mat;
        }

        public Matrix M_find(Matrix _mat, double value)
        {/*Find position with the value in Matrix
    矩阵中寻找特定值位置；有多个相同值，则返回多个位置；
    （Matrix格式返回，为一行存储中的序号）；
    eg.
    [code]
    // define mat_1
        MATRIX_TYPE _mat_1[2][2] = { 1,0,0,1 };
        int row = sizeof(_mat_1) / sizeof(_mat_1[0]);
        int column = sizeof(_mat_1[0]) / sizeof(_mat_1[0][0]);
        Matrix*  mat_1 = Matrix_gen(row,column,(double *)_mat_1);
    // find mat_1
        M_print(M_find(mat_1,1));
    [output]
    >>Matrix_b380c0:
    0.00
    3.00
    [explain]
    即对应 mat_1 { 1,0,0,1 }中第0号、3号元素，值为1。
    */
            int size_mat = (_mat.row) * (_mat.column);
            int[] position = new int[size_mat];

            int num = 0, temp_column, temp_row, i;
            for (i = 0; i < size_mat; i++)
            {
                if (_mat.data[i] == value)
                {
                    position[num] = i;
                    num = num + 1;
                }
            }
            double[] data = new double[num];
            for (i = 0; i < num; i++)
            {
                temp_column = position[i] % (_mat.column);
                temp_row = (position[i] - temp_column) / (_mat.column);
                data[i] = temp_column * (_mat.row) + temp_row;
            }
            Matrix mat_result = new Matrix();
            mat_result.row = num;
            mat_result.column = 1;
            mat_result.data = data;
            
            return mat_result;
        }

        public static Matrix M_sum(Matrix _mat)
        {/*Matrix column sum / Vector element sum
    矩阵按列求和/向量元素和*/
            Matrix mat_result = new Matrix();
            int row = _mat.row, column = _mat.column;
            int i, j, size = row * column;
            if (row == 1 || column == 1)
            {

                double[] data = new double[1];
                data[0] = 0;
                for (i = 0; i < (size); i++)
                {
                    data[0] = data[0] + _mat.data[i];
                }
                mat_result.row = 1;
                mat_result.column = 1;
                mat_result.data = data;
            }
            else
            {
                double[] data = new double[column];
                for (i = 0; i < (column); i++)
                {
                    data[i] = 0;
                    for (j = 0; j < (row); j++)
                    {
                        data[i] = data[i] + _mat.data[j * column + i];
                    }
                }
                mat_result.row = 1;
                mat_result.column = column;
                mat_result.data = data;
            }
            return mat_result;
        }

        public static Matrix M_numul(Matrix _mat, double _num)
        {/*Matrix Multiply
    矩阵数乘*/
            double[] data = _mat.data;
            int Size_mat = (_mat.row) * (_mat.column), i;
            for (i = 0; i < Size_mat; i++)
            {
                data[i] = data[i] * _num;
            }
            return _mat;
        }
        public static Matrix M_min(Matrix _mat)
        {/*Matrix minimum row position / Vector minimum element position
    矩阵按列最小行位置/向量最小元素位置*/
            Matrix mat_result = new Matrix();

            int row = _mat.row, column = _mat.column;
            int i, j, size = row * column;
            if (row == 1 || column == 1)
            {
                double[] data = new double[1];
                data[0] = Min_position(_mat.data, size);
                mat_result.row = 1;
                mat_result.column = 1;
                mat_result.data = data;
            }
            else
            {
                double[] data = new double[column];
                double[] temp_data = new double[row];
                for (i = 0; i < (column); i++)
                {
                    for (j = 0; j < (row); j++)
                    {
                        temp_data[j] = _mat.data[j * column + i];
                    }
                    data[i] = Min_position(temp_data, row);
                }
                mat_result.row = 1;
                mat_result.column = column;
                mat_result.data = data;
            }
            return mat_result;
        }

        public static int Min_position(double[] data, int size)
        {/*Find min-position in a MATRIX_TYPE[*]
    在MATRIX_TYPE[*]列表中，找到最小值位置（第一个最小值位置）*/
            double Val_min = data[size - 1];
            int min_position = size - 1, i;
            for (i = size - 2; i >= 0; i--)
            {
                if (data[i] <= Val_min)
                {
                    Val_min = data[i];
                    min_position = i;
                }
            }
            return min_position;
        }

        public static Matrix M_max(Matrix _mat)
        {/*Matrix maximum row position / Vector maximum element position
    矩阵按列最大行位置/向量最大元素位置*/
            Matrix _mat_ = MatrixCopy(_mat);
            _mat_ = MNumul(_mat_, -1);
            Matrix mat_result = M_min(_mat_);
            return mat_result;
        }

        Matrix M_minax_val(Matrix _mat, Matrix _mat_position)
        {/*use _mat_position(vector)
    to get the value of the specified row position of each column of the matrix (_mat)
    矩阵各列指定行位置的值*/

            Matrix mat_val = new Matrix();
            mat_val.row = _mat_position.row;
            mat_val.column = _mat_position.column;
            int i, temp, size_mat = (_mat_position.row) * (_mat_position.column);
            double[] data = new double[size_mat];
            for (i = 0; i < size_mat; i++)
            {
                temp = ((int)_mat_position.data[i]);
                data[i] = _mat.data[temp * (_mat.column) + i];
            }
            mat_val.data = data;
            return mat_val;
        }

        Matrix M_logic_equal(Matrix _mat, double value)
        {/*Compare each position of the matrix with the given value
    (return the matrix, the value is 0/1)
    矩阵各位置与给定值比较，(返回矩阵,取值0/1)*/
            int size_mat = (_mat.row) * (_mat.column), i;
            Matrix mat_logic = new Matrix();
            mat_logic.row = (_mat.row);
            mat_logic.column = (_mat.column);

            double[] data = new double[size_mat];
            for (i = 0; i < size_mat; i++)
            {
                if (_mat.data[i] == value)
                {
                    data[i] = 1;
                }
                else
                {
                    data[i] = 0;
                }
            }
            mat_logic.data = data;
            return mat_logic;
        }

        Matrix M_logic(Matrix _mat_left, Matrix _mat_right, int Operation)
        {/*Logical operation
     of corresponding positions of two matrices
    两矩阵对应位置逻辑运算
    */
            Matrix mat_logic = new Matrix();
            if (_mat_right!=null)
            {
                if (_mat_left.row != _mat_right.row)
                {
                    Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_logic_012)_mat_left->row != _mat_right->row\n");
                }
                if (_mat_left.column != _mat_right.column)
                {
                    Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_logic_013)_mat_left->column != _mat_right->column\n");
                }
            }

            int size_mat = (_mat_left.row) * (_mat_left.column), i;
            mat_logic.row = (_mat_left.row);
            mat_logic.column = (_mat_left.column);
            double[] data = new double[size_mat];
            switch (Operation)
            {
                case 1:
                    for (i = 0; i < size_mat; i++)
                    {
                        if ((_mat_left.data[i] == 0) || (_mat_right.data[i] == 0))
                        {
                            data[i] = 0;
                        }
                        else
                        {
                            data[i] = 1;
                        }
                    }
                    break;
                case 0:
                    for (i = 0; i < size_mat; i++)
                    {
                        if ((_mat_left.data[i] != 0) || (_mat_right.data[i] != 0))
                        {
                            data[i] = 1;
                        }
                        else
                        {
                            data[i] = 0;
                        }
                    }
                    break;
                case -1:
                    for (i = 0; i < size_mat; i++)
                    {
                        if (_mat_left.data[i] == 0)
                        {
                            data[i] = 1;
                        }
                        else
                        {
                            data[i] = 0;
                        }
                    }
                    break;
                default:
                    Console.WriteLine("@ERROR: Operation Wrong(Dont Exist)!\n\tDetails:(M_logic_014)Operation_Wrong !\n");
                    break;
            }
            mat_logic.data = data;
            return mat_logic;
        }
        Matrix M_pmuldiv(Matrix _mat_left, Matrix _mat_right, int operation)
        {/*Point Mul and Div
    矩阵点乘/点除*/
            Matrix mat_result = new Matrix();
            if (_mat_left.row != _mat_right.row)
            {
                Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_pmuldiv_015)_mat_left->row != _mat_right->row\n");
            }
            if (_mat_left.column != _mat_right.column)
            {
                Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_pmuldiv_016)_mat_left->column != _mat_right->column\n");
            }
            int i, size_mat = (_mat_left.row) * (_mat_left.column);
            mat_result.row = (_mat_left.row);
            mat_result.column = (_mat_left.column);
            double[] data = new double[size_mat];
            if (operation == 1)
            {
                for (i = 0; i < size_mat; i++)
                {
                    data[i] = (_mat_left.data[i]) * (_mat_right.data[i]);
                }
            }
            else
            {
                if (operation == -1)
                {
                    for (i = 0; i < size_mat; i++)
                    {
                        if (_mat_right.data[i] != 0)
                        {
                            data[i] = (_mat_left.data[i]) / (_mat_right.data[i]);
                        }
                        else
                        {
                            data[i] = 1000;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("@ERROR: Operation Wrong(Dont Exist)!\n\tDetails:(M_pmuldiv_017)Operation_Wrong !\n");
                }
            }
            mat_result.data = data;
            return mat_result;
        }

        Matrix M_setval(Matrix _mat_ini, Matrix _mat_val, Matrix _mat_order, int model)
        {/*Mat Set Value
使用矩阵传递数据，给矩阵指定位置赋值
*/
            int i, temp, size_ini = (_mat_ini.column) * (_mat_ini.row);
            int size_val = (_mat_val.column) * (_mat_val.row);
            int size_order = (_mat_order.column) * (_mat_order.row);
            if (model == 1)
            {/*_ORD4INI_*/
                for (i = 0; i < size_order; i++)
                {
                    if ((_mat_order.data[i]) < size_ini)
                    {
                        if (i < size_val)
                        {
                            temp = ((int)_mat_order.data[i]);
                            _mat_ini.data[temp] = _mat_val.data[i];
                        }
                        else
                        {
                            Console.WriteLine("@ERROR: Mat_order lager than Mat_val !\n\tDetails:(M_setval_019)Mat_order_Size_Wrong !\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("@ERROR: Mat_order lager than Mat_ini !\n\tDetails:(M_setval_018)Mat_order_Size_Wrong !\n");
                    }
                }
            }
            else
            {/*_ORD4VAL_*/
                for (i = 0; i < size_ini; i++)
                {
                    if ((i) < size_order)
                    {
                        temp = ((int)_mat_order.data[i]);
                        if (temp < size_val)
                        {
                            _mat_ini.data[i] = _mat_val.data[temp];
                        }
                        else
                        {
                            Console.WriteLine("@ERROR: Mat_order lager than Mat_val !\n\tDetails:(M_setval_019)Mat_order_Size_Wrong !\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("@ERROR: Mat_ini lager than Mat_order !\n\tDetails:(M_setval_020)Mat_ini_Size_Wrong !\n");
                    }
                }
            }

            return _mat_ini;
        }

        Matrix M_numul_m(Matrix _mat, Matrix _mat_multi)
        {/*Matrix Multiply
    矩阵数乘 使用矩阵对于矩阵,对于行进行操作
    _mat_result = _mat_left*_mat_right */
            double[] data = _mat.data;
            int Size_mat = (_mat.row) * (_mat.column), i, j, temp;
            int row = _mat.row;
            int column = _mat.column;
            double Multi;
            for (i = 0; i < row; i++)
            {
                Multi = _mat_multi.data[i];
                for (j = 0; j < column; j++)
                {
                    temp = i * column + j;
                    data[temp] = data[temp] * Multi;
                }
            }
            return _mat;
        }

        public static MatrixEigenStructSingle M_eigen_max(Matrix _mat)
        {    /*Matrix Max Eigenvalue(vec)
    求解矩阵最大特征值（幂法）
    _mat_result = Max_eigenvalue(_mat)
    幂法-算法参考：https://max.book118.com/html/2017/0527/109650252.shtm*/
            MatrixEigenStructSingle M_eigen_max = null;
            if (_mat.column == _mat.row)
            {
                Matrix mat_z = M_Ones(_mat.column, 1), mat_temp_1 = null, mat_temp_2 = null;
                Matrix mat_y = null, mat_z_gap = null;
                double m_value = 0, mat_z_gap_norm = 1;
                double deta = 1e-7; //精度设置
                int temp_num = 0;

                while (mat_z_gap_norm > deta)
                {
                    mat_y = M_mul(_mat, mat_z);
                    mat_temp_1 = M_max(mat_y);//需要释放结果空间
                    temp_num = ((int)(mat_temp_1).data[0]);
                    m_value = mat_y.data[temp_num];
                    mat_temp_2 = mat_z;//需要释放结果空间
                    mat_z = M_numul(mat_y, 1 / m_value);
                    mat_z_gap = M_add_sub(1, mat_z, 1, mat_temp_2);//需要释放结果空间
                    mat_z_gap_norm = M_norm(mat_z_gap, 2);

                }


                M_eigen_max = new MatrixEigenStructSingle();
                M_eigen_max.eigen_value = m_value;
                M_eigen_max.eigen_matrix = mat_z;
            }
            else
            {
                Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_eigen_max_021)Mat->column != Mat->row!\n\t\t(For eigen, the Matrix must be a square matrix!)\n");
            }
            return M_eigen_max;
        }

        public static Matrix[] M_eigen(Matrix _mat)
        {
            Matrix[] M_array_eigen_vec = null;
            if (_mat.column == _mat.row)
            {
                M_array_eigen_vec = new Matrix[2]; // 保存Q/R矩阵地址
                Matrix eigen_value = M_eigen_val(_mat);
                M_array_eigen_vec[0] = eigen_value;
                int eigen_count, dim = _mat.column, i, j, ik, jk;
                Matrix eigen_vector = null, _mat_ = null;
                eigen_vector = M_Zeros(dim, dim);// 生成特征向量
                M_array_eigen_vec[1] = eigen_vector;
                double eigen_value_temp;
                double coe; // core of elements, 对角线元素/中心元素
                for (eigen_count = 0; eigen_count < dim; eigen_count++)
                {
                    _mat_ = MatrixCopy(_mat);
                    eigen_value_temp = eigen_value.data[eigen_count];
                    // (A-lamda*I)
                    for (i = 0; i < dim; i++)
                    {
                        _mat_.data[i * _mat_.column + i] -= eigen_value_temp; // 注意: 这里计算 (A-lamda*I), 当A为I/diag时，可能存在问题;
                    }
                    // 矩阵化为阶梯型矩阵(归一性): 对角线值为1
                    for (i = 0; i < dim - 1; i++)
                    {
                        coe = _mat_.data[i * dim + i];
                        for (j = i; j < dim; j++)
                        {
                            _mat_.data[i * dim + j] /= coe; //让对角线元素归一化
                        }
                        for (ik = i + 1; ik < dim; ik++)
                        {
                            coe = _mat_.data[ik * dim + i];
                            for (jk = i; jk < dim; jk++)
                            {
                                _mat_.data[ik * dim + jk] -= coe * _mat_.data[i * dim + jk];
                            }
                        }
                    }
                    // 让最后一行为1
                    double sum1 = 1;
                    eigen_vector.data[(dim - 1) * dim + eigen_count] = 1;
                    for (ik = dim - 2; ik >= 0; ik--)
                    {
                        double sum2 = 0;
                        for (jk = ik + 1; jk < dim; jk++)
                        {
                            sum2 += _mat_.data[ik * dim + jk] * eigen_vector.data[jk * dim + eigen_count];
                        }
                        sum2 = -sum2 / _mat_.data[ik * dim + ik];
                        sum1 += sum2 * sum2;
                        eigen_vector.data[ik * dim + eigen_count] = sum2;
                    }

                    sum1 = Math.Sqrt(sum1);//当前列的模
                    for (int b = 0; b < dim; b++)
                    {
                        // 向量单位化
                        eigen_vector.data[b * dim + eigen_count] /= sum1;
                    }
                }

            }
                return M_array_eigen_vec;
        }

        public static Matrix householder(Matrix _x)
        {
            Matrix H = null;
            Matrix y = M_Zeros(_x.row, _x.column);
            y.data[0] = M_norm(_x, 2);
            Matrix w = null;
            if (_x.data[0] > 0)
            {
                w = M_add_sub(1, _x, -1, y);
                M_numul(w, 1 / M_norm(w, 2));
            }
            else
            {
                w = M_add_sub(1, _x, 1, y);
                M_numul(w, 1 / M_norm(w, 2));
            }
            Matrix I = M_I(_x.row);
            Matrix w_T = MT(w);
            Matrix M_dot = M_mul(w, w_T);
            H = M_add_sub(1, I, 2, M_dot);
            return H;
        }

        Matrix M_householder(Matrix _mat)
        {
            Matrix h_Mat = null;
            if (_mat.column == _mat.row)
            {
                int i, j, k, dim = _mat.column;
                Matrix Ri = MatrixCopy(_mat);
                Matrix temp = null;
                Matrix Q, Qi, Hi;
                Q = null;
                for (i = 1; i < dim; i++)
                {
                    Matrix x = MCut(_mat, i + 1, -1, i, i);
                    // householder 具体计算
                    Hi = householder(x);
                    Qi = M_I(dim);
                    for (j = 0; j < dim - i; j++)
                    { // Qi[i:, i:] = Hi
                        for (k = 0; k < dim - i; k++)
                        {
                            Qi.data[(j + i) * dim + (k + i)] = Hi.data[j * (dim - i) + k];
                        }
                    }
                    if (i == 1)
                    {
                        Q = MatrixCopy(Qi);
                    }
                    else
                    {
                        temp = Q;
                        Q = M_mul(Qi, temp);
                        
                    }
                    temp = Ri;
                    Ri = M_mul(Qi, Ri);
                  
                    temp = Ri;
                    Ri = M_mul(Ri, Qi);

                }
                h_Mat = Ri;

            }
            else
            {
                Console.WriteLine("@ERROR: Matrix_Dimensions Wrong!\n\tDetails:(M_householder_027)Mat->column != Mat->row!\n\t\t(For Matrix_householder, the Matrix must be a square matrix!)\n");
            }
            return h_Mat;
        }

        public static Matrix[] M_QR(Matrix _mat)
        {
            Matrix[] M_array_Q_R = new Matrix[2];
            M_array_Q_R[0] = null;
            M_array_Q_R[1] = null;
            int i, j, k, dim = _mat.row;
            Matrix Q = null, D = null, Qi = null, Hi = null, x = null, temp_1 = null, temp_2 = null;
            Matrix Ri = MatrixCopy(_mat); // 注意
            for (i = 0; i < dim; i++)
            {
                x = MCut (_mat, i + 1, -1, i + 1, i + 1);
                Hi = householder(x);

                // Ri[i:, i:] = np.dot(Hi, Ri[i:, i:])
                temp_1 = MCut (Ri, i + 1, -1, i + 1, -1);
                temp_2 = M_mul(Hi, temp_1);
                for (j = 0; j < dim - i; j++)
                {
                    for (k = 0; k < dim - i; k++)
                    {
                        Ri.data[(j + i) * dim + (k + i)] = temp_2.data[j * (dim - i) + k];
                    }
                }

                Qi = M_I(dim);
                for (j = 0; j < dim - i; j++)
                { // Qi[i:, i:] = Hi
                    for (k = 0; k < dim - i; k++)
                    {
                        Qi.data[(j + i) * dim + (k + i)] = Hi.data[j * (dim - i) + k];
                    }
                }

                if (i == 0)
                {
                    Q = MatrixCopy(Qi);
                }
                else
                {
                    temp_1 = Q;
                    Q = M_mul(Qi, temp_1);
                }
            }
            D = M_I(dim);
            for (i = 0; i < dim; i++)
            {
                D.data[i] = (Ri.data[i * dim + i] < 0) ? -1 : 1;
            }
            M_array_Q_R[1] = M_mul(D, Ri);
            temp_1 = MT(Q);
            temp_2 = M_Dia_Inv(D);
            M_array_Q_R[0] = M_mul(temp_1, temp_2);
            return M_array_Q_R;
        }

        public static Matrix M_eigen_val(Matrix _mat)
        {
            double[] eigen_val = null;
            Matrix[] M_array_Q_R = null; // 保存Q/R矩阵地址

            double eps = 1e-5, delta = 1; // 设置计算误差
            int i, dim = _mat.row, epoch = 0;
            Matrix Ak0, Ak, Qk, Rk, M_eigen_val;
            Ak = MatrixCopy(_mat);
            while ((delta > eps) && (epoch < (int)1e+5))
            {
                M_array_Q_R = M_QR(Ak);
                Qk = M_array_Q_R[0];
                Rk = M_array_Q_R[1];
                Ak0 = Ak;
                Ak = M_mul(Rk, Qk);
                delta = 0;
                for (i = 0; i < dim; i++)
                {
                    delta += Math.Abs(Ak.data[i * dim + i] - Ak0.data[i * dim + i]);
                }

                if (epoch == 1)
                {
                    progress_bar(epoch, (int)1e+5);
                }
                else
                {
                    
                }

                epoch++;
            }
            if (epoch >= (int)1e+5)
            {
               // printf("\n>>ATTENTION: QR Decomposition end with delta = %.3e!(epoch=%d, eps=%.2e)\n", delta, _MAX_LOOP_NUM_, eps);
            }
            M_eigen_val = new Matrix();
            M_eigen_val.column = dim;
            M_eigen_val.row = 1;
            eigen_val = new double[dim];
            for (i = 0; i < dim; i++)
            {
                eigen_val[i] = Ak.data[i * dim + i];
            }
            M_eigen_val.data = eigen_val;

            //(_DETAILED_ >= 2) ? printf("...END...\n>>Eigen_Value = (Matrix_%x)\n", M_eigen_val) : 0;
            return M_eigen_val;
        }

        static void progress_bar(int count, int total)
        {
            double num = (int)((1.0 * count / total) * 50);

            Console.WriteLine("%% %.2f[", num * 2);
            for (int i = 0; i < 50; i++)
            {
                if (i < num)
                {
                    Console.WriteLine(">");
                }
                else
                {
                    Console.WriteLine(" ");
                }
            }
            Console.WriteLine("]\r");
        }
    }
}
