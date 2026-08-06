using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SystemParametersViewer
{
    internal class PropertyValueCellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalCellTemplate { get; set; }
        public DataTemplate NullCellTemplate { get; set; }
        public DataTemplate TextCellTemplate { get; set; }
        public DataTemplate ColorCellTemplate { get; set; }
        public DataTemplate BrushCellTemplate { get; set; }
        public DataTemplate ResourceKeyCellTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item)
            {
                case null:
                    return NullCellTemplate;

                case string text:
                    return TextCellTemplate;

                case Color color:
                    return ColorCellTemplate;

                case Brush brush:
                    return BrushCellTemplate;

                case ResourceKey resourceKey:
                    return ResourceKeyCellTemplate;

                default:
                    return NormalCellTemplate;
            }
        }
    }
}
