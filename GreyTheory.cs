using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


class GreyTheory
{
    public double GM11(List<double> _original_list)
    {
        //開始Graey theory 建模過程
        //1. 原始數列
        Vector<double> _Original_X = Vector<double>.Build.Dense(_original_list.Count);
        for (int _i = 0; _i < _original_list.Count; _i++)
            _Original_X[_i] = _original_list.ElementAt(_i);

        //2. 累加生成數列
        Vector<double> _Accu_X = Vector<double>.Build.Dense(_original_list.Count);
        for (int _i = 0; _i < _original_list.Count; _i++)
        {
            _Accu_X[_i] = 0;
            for (int _j = 0; _j <= _i; _j++)
                _Accu_X[_i] += _original_list.ElementAt(_j);
        }

        //3.累加矩陣
        Matrix<double> _AccuMatrix = Matrix<double>.Build.Dense((_original_list.Count - 1), 2, 1);
        for (int _i = 0; _i < (_original_list.Count - 1); _i++)
            _AccuMatrix[_i, 0] = (-0.5) * (_Accu_X[_i] + _Accu_X[_i + 1]);

        //4.常數向量
        Vector<double> _ConstantVector = Vector<double>.Build.Dense(_original_list.Count - 1);
        for (int _i = 0; _i < (_original_list.Count - 1); _i++)
            _ConstantVector[_i] = _Original_X[_i + 1];

        //5.係數向量
        var _CoffecientVector = (_AccuMatrix.Transpose() * _AccuMatrix).Inverse() * _AccuMatrix.Transpose() * _ConstantVector;

        //6.Predict ...
        Vector<double> _Predict_X = Vector<double>.Build.Dense(_original_list.Count);
        for (int _i = 0; _i < _original_list.Count; _i++)
        {
            //_Predict_X[_i] = (1 - Math.Exp(-_CoffecientVector[0])) * (_Original_X[0] + _CoffecientVector[1] / _CoffecientVector[0]) * Math.Exp(_CoffecientVector[0] * _i);
            _Predict_X[_i] = (1 - Math.Exp(_CoffecientVector[0])) * (_Original_X[0] - _CoffecientVector[1] / _CoffecientVector[0]) * Math.Exp(-_CoffecientVector[0] * _i);
        }

        //7.修正.
        double _FixCoefficient = (_CoffecientVector[1] / _CoffecientVector[0]) - (_Original_X[0] + (_CoffecientVector[1] / _CoffecientVector[0])) * Math.Exp(-_CoffecientVector[0]);
        Vector<double> _FixNum = Vector<double>.Build.Dense(_original_list.Count);

        //6.Finally
        //var _PredictorValue = (1 - Math.Exp(_CoffecientVector[0])) * (_Original_X[0] - _CoffecientVector[1] / _CoffecientVector[0]) * Math.Exp(-_CoffecientVector[0] * _original_list.Count);


        return (1 - Math.Exp(_CoffecientVector[0])) * (_Original_X[0] - _CoffecientVector[1] / _CoffecientVector[0]) * Math.Exp(-_CoffecientVector[0] * _original_list.Count);
    }
}

