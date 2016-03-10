using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace YLP.UWP.Triggers
{
    public class MasterDetailStateTrigger : StateTriggerBase, ITriggerValue
    {
        public MasterDetailStateTrigger()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                var weakEvent = new WeakEventListener<MasterDetailStateTrigger, ApplicationView, object>(this)
                {
                    OnEventAction = (instance, source, eventArgs) => MasterDetailStatetateTrigger_MasterDetailStateChanged(source, eventArgs),
                    OnDetachAction = (instance, weakEventListener) => ApplicationView.GetForCurrentView().VisibleBoundsChanged -= weakEventListener.OnEvent
                };

                ApplicationView.GetForCurrentView().VisibleBoundsChanged += weakEvent.OnEvent;
            }
        }

        private void MasterDetailStatetateTrigger_MasterDetailStateChanged(ApplicationView sender, object args)
        {
            UpdateTrigger();
        }

        private void UpdateTrigger()
        {
          //  IsActive = GetMasterDetailState() == MasterDetailState;
        }

        public MasterDetailState MasterDetailState
        {
            get
            {
                return (MasterDetailState)GetValue(MasterDetailStateProperty);
            }
            set
            {
                SetValue(MasterDetailStateProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MasterDetailState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MasterDetailStateProperty =
            DependencyProperty.Register("MasterDetailState", typeof(MasterDetailState), typeof(MasterDetailStateTrigger), new PropertyMetadata(MasterDetailState.Wide, OnMasterDetailStatePropertyChanged));

        private static void OnMasterDetailStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MasterDetailStateTrigger)d;
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                obj.UpdateTrigger();
            }
        }

        public object DetailContent
        {
            get
            {
                return (object)GetValue(DetailContentProperty);
            }
            set
            {
                SetValue(DetailContentProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for DetailContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DetailContentProperty =
            DependencyProperty.Register("DetailContent", typeof(object), typeof(MasterDetailStateTrigger), new PropertyMetadata(null, new PropertyChangedCallback(OnValuePropertyChanged)));

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (MasterDetailStateTrigger)d;
            obj.UpdateTrigger();
        }


        //internal MasterDetailState GetMasterDetailState()
        //{
        //    System.Diagnostics.Debug.WriteLine("DetailContent为空：" + (DetailContent == null).ToString());

        //    //第一种 窄屏模式 DetailFrame为空
        //    if (Window.Current.Bounds.Width < 720)
        //    {
        //        System.Diagnostics.Debug.WriteLine("VisibleBounds.Width：" + ApplicationView.GetForCurrentView().VisibleBounds.Width);
        //        System.Diagnostics.Debug.WriteLine("Window.Current.Bounds：" + Window.Current.Bounds.Width);

        //        MVVMPage detailPage = (MVVMSidekick.Views.MVVMPage)DetailContent;
        //        if (detailPage != null)
        //        {
        //            if (detailPage.BaseUri.ToString() == "ms-appx:///BlankPage.xaml")
        //            {
        //                System.Diagnostics.Debug.WriteLine("触发NarrowAndBlankDetail模式");

        //                return MasterDetailState.NarrowAndBlankDetail;
        //            }

        //            System.Diagnostics.Debug.WriteLine("触发NarrowAndNoBlankDetail模式");
        //            return MasterDetailState.NarrowAndNoBlankDetail;
        //        }

        //        return MasterDetailState.NarrowAndBlankDetail;
        //    }

        //    System.Diagnostics.Debug.WriteLine("触发Wide模式");

        //    return MasterDetailState.Wide;
        //}


        #region ITriggerValue

        private bool _isActive;

        /// <summary>
        /// Gets a value indicating whether this trigger is active.
        /// </summary>
        /// <value><c>true</c> if this trigger is active; otherwise, <c>false</c>.</value>
        public bool IsActive
        {
            get { return _isActive; }
            private set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    base.SetActive(value);

                    IsActiveChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Occurs when the <see cref="IsActive" /> property has changed.
        /// </summary>
        public event EventHandler IsActiveChanged;

        #endregion ITriggerValue
    }

    public enum MasterDetailState
    {
        /// <summary>
        /// narrow and a blank detail page
        /// </summary>
        NarrowAndBlankDetail,

        /// <summary>
        /// narrow and detail page is not blank
        /// </summary>
        NarrowAndNoBlankDetail,

        /// <summary>
        /// wide
        /// </summary>
        Wide
    }
}
