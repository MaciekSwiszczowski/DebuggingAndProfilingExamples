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

        private void ContentPresenter_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}