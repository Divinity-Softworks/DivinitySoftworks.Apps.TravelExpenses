using System.Windows;
using System.Windows.Controls;

namespace DivinitySoftworks.Apps.TravelExpenses.UI.Components {

    public partial class Background : Grid {
        public Background() {
            InitializeComponent();
        }

        public object LeftContent {
            get { return GetValue(LeftContentProperty); }
            set { SetValue(LeftContentProperty, value); }
        }

        public static readonly DependencyProperty LeftContentProperty = DependencyProperty.Register(nameof(LeftContent), typeof(object), typeof(Background), null);

        public object MiddleContent {
            get { return GetValue(MiddleContentProperty); }
            set { SetValue(MiddleContentProperty, value); }
        }

        public static readonly DependencyProperty MiddleContentProperty = DependencyProperty.Register(nameof(MiddleContent), typeof(object), typeof(Background), null);


        public object RightContent {
            get { return GetValue(RightContentProperty); }
            set { SetValue(RightContentProperty, value); }
        }

        public static readonly DependencyProperty RightContentProperty = DependencyProperty.Register(nameof(RightContent), typeof(object), typeof(Background), null);

    }
}
