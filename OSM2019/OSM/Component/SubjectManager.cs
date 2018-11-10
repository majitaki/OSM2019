using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class SubjectManager
    {
        List<string> SubjectList;
        List<OpinionConversion> OpinionConvList;

        public SubjectManager()
        {
            this.SubjectList = new List<string>();
            this.OpinionConvList = new List<OpinionConversion>();
        }

        public SubjectManager AddSubject(string subject_str)
        {
            this.SubjectList.Add(subject_str);
            return this;
        }

        public SubjectManager AddConvMatrix(string from_subject, string to_subject, Matrix<double> conv_matrix)
        {
            this.OpinionConvList.Add(new OpinionConversion(from_subject, to_subject, conv_matrix));
            return this;
        }

        public Matrix<double> GetConvMatrix(string from_subject, string to_subject)
        {
            var conv_matrix = this.OpinionConvList.First(op_conv => op_conv.FromSubject == from_subject && op_conv.ToSubject == to_subject).ConvMatrix;
            return conv_matrix;
        }

        public Matrix<double> GetInverseConvMatrix(string from_subject, string to_subject)
        {
            var inv_conv_matrix = this.OpinionConvList.First(op_conv => op_conv.FromSubject == from_subject && op_conv.ToSubject == to_subject).InverseConvMatrix;
            return inv_conv_matrix;
        }

        class OpinionConversion
        {
            public OpinionConversion(string from_subject, string to_subject, Matrix<double> conv_matrix)
            {
                this.FromSubject = from_subject;
                this.ToSubject = to_subject;
                this.ConvMatrix = conv_matrix;
            }

            public string FromSubject;
            public string ToSubject;

            Matrix<double> _conv_matrix;
            public Matrix<double> ConvMatrix
            {
                get
                {
                    return this._conv_matrix;
                }

                set
                {
                    this._conv_matrix = value;
                    this.InverseConvMatrix = this._conv_matrix.Inverse();
                }
            }

            public Matrix<double> InverseConvMatrix;
        }
    }
}
