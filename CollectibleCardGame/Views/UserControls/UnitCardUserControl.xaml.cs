using System.Windows;
using System.Windows.Controls;

namespace CollectibleCardGame.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для UnitCardUserControl.xaml
    /// </summary>
    public partial class UnitCardUserControl : UserControl
    {
        public UnitCardUserControl()
        {
            InitializeComponent();

            //this.MouseLeftButtonDown += new MouseButtonEventHandler(Control_MouseLeftButtonDown);
            //this.MouseLeftButtonUp += new MouseButtonEventHandler(Control_MouseLeftButtonUp);
            //this.MouseMove += new MouseEventHandler(Control_MouseMove);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //double a = Convert.ToDouble(UserControl.ActualHeightProperty) / 25.625;
            //double b = Convert.ToDouble(UserControl.ActualHeightProperty)/ 34.17;
            //double с = Convert.ToDouble(UserControl.ActualHeightProperty) / 9.76;
            //UnitName.FontSize = a;
            //UnitStory.FontSize = b;
            //UnitCost.FontSize=UnitAttck.FontSize=UnitHealth.FontSize = с; //+высота ширина кругов(бордеров)
        }


        //protected bool isDragging;
        //private Point clickPosition;

        //private void Control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    isDragging = true;
        //    var draggableControl = sender as UserControl;
        //    clickPosition = e.GetPosition(this);
        //    draggableControl.CaptureMouse();
        //}

        //private void Control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    isDragging = false;
        //    var draggable = sender as UserControl;
        //    draggable.ReleaseMouseCapture();
        //}

        //private void Control_MouseMove(object sender, MouseEventArgs e)
        //{
        //    var draggableControl = sender as UserControl;

        //    if (isDragging && draggableControl != null)
        //    {
        //        Point currentPosition = e.GetPosition(this.Parent as UIElement);

        //        var transform = draggableControl.RenderTransform as TranslateTransform;
        //        if (transform == null)
        //        {
        //            transform = new TranslateTransform();
        //            draggableControl.RenderTransform = transform;
        //        }

        //        transform.X = currentPosition.X - clickPosition.X;
        //        transform.Y = currentPosition.Y - clickPosition.Y;
        //    }
        //}
    }
}