using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XphenoApp.Controls;
using XphenoApp.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(BorderlessEntryRenderer))]
namespace XphenoApp.Droid.CustomRenderer
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (e.NewElement.Keyboard != Keyboard.Numeric)
                {
                    Control.InputType = Android.Text.InputTypes.ClassText | Android.Text.InputTypes.TextVariationVisiblePassword | Android.Text.InputTypes.TextFlagMultiLine;
                }
                else if (e.NewElement.Keyboard == Keyboard.Numeric)
                {
                    Control.InputType = Android.Text.InputTypes.ClassNumber;
                }
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetPadding(0, 0, 0, 0);
                Control.Gravity = GravityFlags.Left;
                Control.SetIncludeFontPadding(false);
                //Control.SetMinimumWidth(800);
                Control.SetBackground(gd);
            }
        }
    }
}

