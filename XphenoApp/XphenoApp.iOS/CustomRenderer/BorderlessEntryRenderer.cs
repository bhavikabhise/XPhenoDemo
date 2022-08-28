using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XphenoApp.Controls;
using XphenoApp.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(BorderlessEntryRenderer))]
namespace XphenoApp.iOS.CustomRenderer
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (e.NewElement.Keyboard != Keyboard.Numeric)
                {
                    Control.KeyboardType = UIKeyboardType.AsciiCapable;
                }
                else if (e.NewElement.Keyboard == Keyboard.Numeric)
                {
                    Control.KeyboardType = UIKeyboardType.NumberPad;
                }
            }
        }
    }
}

