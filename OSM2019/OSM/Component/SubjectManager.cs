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
        List<OpinionConversion> OpinionConvList;
        public OpinionEnvironment OSM_Env { get; private set; }
        public List<OpinionSubject> Subjects { get; private set; }

        public SubjectManager()
        {
            this.OpinionConvList = new List<OpinionConversion>();
            this.OSM_Env = new OpinionEnvironment();
            this.Subjects = new List<OpinionSubject>();
        }

        public SubjectManager AddSubject(OpinionSubject subject)
        {
            this.Subjects.Add(subject);
            return this;
        }

        public SubjectManager RegistConversionMatrix(OpinionSubject from_subject, OpinionSubject to_subject, Matrix<double> conv_matrix)
        {
            from_subject.SetSubjectManager(this);
            to_subject.SetSubjectManager(this);
            this.OpinionConvList.Add(new OpinionConversion(from_subject, to_subject, conv_matrix));
            this.OpinionConvList.Add(new OpinionConversion(to_subject, from_subject, conv_matrix.PseudoInverse()));
            return this;
        }

        public SubjectManager SetEnvironment(OpinionEnvironment env)
        {
            this.OSM_Env = env;
            return this;
        }

        public Matrix<double> GetConversionMatrix(OpinionSubject from_subject, OpinionSubject to_subject)
        {
            return this.OpinionConvList.First(op_conv => op_conv.FromSubject == from_subject && op_conv.ToSubject == to_subject).ConvMatrix;
        }

        class OpinionConversion
        {
            public OpinionSubject FromSubject;
            public OpinionSubject ToSubject;
            public Matrix<double> ConvMatrix;

            public OpinionConversion(OpinionSubject from_subject, OpinionSubject to_subject, Matrix<double> conv_matrix)
            {
                this.FromSubject = from_subject;
                this.ToSubject = to_subject;
                this.ConvMatrix = conv_matrix;
            }

        }
    }
}
