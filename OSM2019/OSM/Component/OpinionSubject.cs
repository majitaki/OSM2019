using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class OpinionSubject
    {
        public string SubjectName { get; private set; }
        public int SubjectDimSize { get; private set; }
        SubjectManager MySubjectManager;

        public OpinionSubject(string subject_name, int subject_dim_size)
        {
            this.SubjectName = subject_name;
            this.SubjectDimSize = subject_dim_size;
        }

        public void SetSubjectManager(SubjectManager subject_manager)
        {
            this.MySubjectManager = subject_manager;
        }

        public Vector<double> ConvertOpinionForSubject(Vector<double> opinion, OpinionSubject to_subject)
        {
            var conv_matrix = this.MySubjectManager.GetConversionMatrix(this, to_subject);
            return conv_matrix * opinion;
        }
    }
}
