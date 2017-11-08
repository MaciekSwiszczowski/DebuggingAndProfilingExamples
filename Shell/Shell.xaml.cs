using System.ComponentModel.Composition;

namespace WpfExamples
{
    [Export(typeof(Shell))]
    public partial class Shell 
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
