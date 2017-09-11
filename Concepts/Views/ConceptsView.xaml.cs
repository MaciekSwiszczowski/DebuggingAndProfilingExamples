using System.ComponentModel.Composition;

namespace Concepts.Views
{
    [Export]
    public partial class ConceptsView 
    {
        public ConceptsView()
        {
            InitializeComponent();
        }
    }
}