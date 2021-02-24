using Atomus.Control;
using Atomus.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Atomus.Windows.Style
{
    public class DefaultStyle : IAction
    {
        private AtomusControlEventHandler beforeActionEventHandler;
        private AtomusControlEventHandler afterActionEventHandler;

        private ResourceDictionary ResourceDictionaryRoot;

        #region Init
        public DefaultStyle()
        {
        }
        #endregion

        #region IO
        object IAction.ControlAction(ICore sender, AtomusControlArgs e)
        {
            string skin;

            try
            {
                this.beforeActionEventHandler?.Invoke(this, e);

                switch (e.Action)
                {
                    case "CreateStyle":
                        this.ResourceDictionaryRoot = (ResourceDictionary)e.Value;

                        skin = Config.Client.GetAttribute("SkinName").ToString();

                        this.CreateStyle(skin);

                        this.SetStyle(skin);
                        //new Task(() => { this.SetStyle(skin); }).RunSynchronously();

                        return null;
                }

                return true;
            }
            finally
            {
                this.afterActionEventHandler?.Invoke(this, e);
            }
        }

        public void CreateStyle(string skin)
        {
            ResourceDictionary resourceDictionary;

            resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri($"pack://application:,,,/Atomus.Windows.Style.DefaultStyle;component/ResourceDictionary{skin}.xaml");

            this.ResourceDictionaryRoot.MergedDictionaries.Add(resourceDictionary);
        }
        #endregion


        #region Event
        event AtomusControlEventHandler IAction.BeforeActionEventHandler
        {
            add
            {
                this.beforeActionEventHandler += value;
            }
            remove
            {
                this.beforeActionEventHandler -= value;
            }
        }
        event AtomusControlEventHandler IAction.AfterActionEventHandler
        {
            add
            {
                this.afterActionEventHandler += value;
            }
            remove
            {
                this.afterActionEventHandler -= value;
            }
        }
        #endregion


        #region Etc
        private async void SetStyle(string skin)
        {
            System.Windows.Style style;
            ImageSource image;


            foreach (var aa in this.ResourceDictionaryRoot.MergedDictionaries[0].Keys)
            {
                if (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] is System.Windows.Style
                    && (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style).TargetType == typeof(Window)
                    && !(aa is string))
                    this.SetStyleWindow(skin, this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style, aa);



                if (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] is System.Windows.Style
                    && (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style).TargetType == typeof(UserControl)
                    && (aa is string))
                    this.SetStyleUserControl(skin, this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style, aa);

                if (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] is System.Windows.Style
                    && (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style).TargetType == typeof(Button)
                    && (aa is string))
                    this.SetStyleButton(skin, this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style, aa);


                if (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] is System.Windows.Style
                    && (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style).TargetType == typeof(TextBox)
                    && (aa is string))
                    ;


                if (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] is System.Windows.Style
                    && (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style).TargetType == typeof(Button) && !(aa is string))
                {

                    try
                    {
                        //image = await this.GetAttributeMediaWebImage("ImageTest");

                        //style = this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style;
                        //style.Setters.Add(new Setter(Button.BackgroundProperty, new ImageBrush(image)));

                        //Task t = new Task(async () =>
                        //{
                        //    image = await this.GetAttributeMediaWebImage("ImageTest");

                        //    style = this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style;
                        //    style.Setters.Add(new Setter(Button.BackgroundProperty, new ImageBrush(image)));
                        //});

                        //t.RunSynchronously();

                    }
                    catch (Exception exception)
                    {
                        DiagnosticsTool.MyTrace(exception);
                    }

                }
            }
        }

        private void SetStyleWindow(string skin, System.Windows.Style style, object key)
        {
            try
            {
                //style.Setters.Add(new Setter(Window.BackgroundProperty, new SolidColorBrush(this.GetAttributeMediaColor($"{skin}.DefaultBrowser.Background"))));
                //style.Setters.Add(new Setter(Window.ForegroundProperty, new SolidColorBrush(this.GetAttributeMediaColor($"{skin}.DefaultBrowser.Foreground"))));
            }
            catch (Exception exception)
            {
                DiagnosticsTool.MyTrace(exception);
            }
        }

        private async void SetStyleUserControl(string skin, System.Windows.Style style, object key)
        {
            string keyString;
            ImageSource image;
            Setter setter;

            if (key is string)
            {
                keyString = key as string;

                switch (keyString)
                {
                    case "UserControlDefaultLogin":
                        //style.Setters.Add(new Setter(UserControl.BackgroundProperty, new SolidColorBrush(this.GetAttributeMediaColor($"{skin}.DefaultBrowser.Background"))));
                        //style.Setters.Add(new Setter(UserControl.ForegroundProperty, new SolidColorBrush(this.GetAttributeMediaColor($"{skin}.DefaultBrowser.Foreground"))));

                        //image = await this.GetAttributeMediaWebImage($"{skin}.UserControlDefaultLogin.Background");
                        //style.Setters.Add(new Setter(Button.BackgroundProperty, new ImageBrush(image)));

                        style.Setters.Add(new Setter(Button.BackgroundProperty, null));

                        Task t = new Task(async () =>
                        {
                            image = await this.GetAttributeMediaWebImage($"{skin}.UserControlDefaultLogin.Background");
                            setter = (Setter)style.Setters[0];
                            setter.Value = new ImageBrush(image);
                        });

                        t.RunSynchronously();

                        break;
                }
            }
            else
            {

            }
        }

        private void SetStyleButton(string skin, System.Windows.Style style, object key)
        {
            string keyString;
            ImageSource image;
            ControlTemplate controlTemplate;

            if (key is string)
            {
                keyString = key as string;

                switch (keyString)
                {
                    case "UserControlDefaultLogin.Button":
                        //style.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(this.GetAttributeMediaColor($"{skin}.UserControlDefaultLogin.Button.Background"))));
                        //style.Setters.Add(new Setter(Button.ForegroundProperty, new SolidColorBrush(this.GetAttributeMediaColor($"{skin}.UserControlDefaultLogin.Button.Foreground"))));

                        //controlTemplate = new ControlTemplate(typeof(Button));

                        //style.Setters.Add(new Setter(System.Windows.Controls.Control.TemplateProperty, controlTemplate));

                        break;
                }
            }
            else
            {

            }
        }




        private bool SetStyle00()
        {
            //bool code = Task.Run(this.SetStyle0).Result;

            //bool code = this.SetStyle0().GetAwaiter().GetResult();
            //return code;


            //var t = this.SetStyle0();
            //Task.WhenAll(t);
            //bool code = t.Result;

            //return code;

            //Task.Run(async () => { await this.SetStyle0(); });
            //return true;

            //Task.Run(() => SetStyle0()).GetAwaiter().GetResult();
            //return true;



            Task<bool> task = Task.Run(() => SetStyle0());
            bool result = task.Result;
            return result;


            //var a = this.SetStyle0();
            //return Task.FromResult(true);

            //await SetStyle0().ConfigureAwait(true);
            //return true;

            //SetStyle0().GetAwaiter().GetResult();

            //if (await this.SetStyle0().AsTask())
            //    return true;

            //return false;
        }
        private bool SetStyle0()
        {
            System.Windows.Style style;
            Task<ImageSource> imageTask;
            ImageSource image;



            foreach (var aa in this.ResourceDictionaryRoot.MergedDictionaries[0].Keys)
            {
                if (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] is System.Windows.Style && (this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style).TargetType == typeof(Button) && !(aa is string))
                {
                    try
                    {
                        //image = await this.GetAttributeMediaWebImage("ImageTest");

                        Task t = new Task(async () =>
                        {
                            image = await this.GetAttributeMediaWebImage("ImageTest");

                            style = this.ResourceDictionaryRoot.MergedDictionaries[0][aa] as System.Windows.Style;
                            style.Setters.Add(new Setter(Button.BackgroundProperty, new ImageBrush(image)));
                        });

                        t.RunSynchronously();

                    }
                    catch (Exception exception)
                    {
                        DiagnosticsTool.MyTrace(exception);
                        return false;
                    }

                }
            }

            //foreach (var aa in this.Resources.MergedDictionaries[0].Values)
            //{
            //    if (aa is System.Windows.Style && (aa as System.Windows.Style).TargetType == typeof(Button))
            //    {
            //        try
            //        {
            //            //image = await this.GetAttributeMediaWebImage("ImageTest");

            //            Task t = new Task(async () =>
            //            {
            //                image = await this.GetAttributeMediaWebImage("ImageTest");

            //                style = aa as System.Windows.Style;
            //                style.Setters.Add(new Setter(Button.BackgroundProperty, new ImageBrush(image)));
            //            });

            //            t.RunSynchronously();

            //        }
            //        catch (Exception exception)
            //        {
            //            DiagnosticsTool.MyTrace(exception);
            //            return false;
            //        }

            //    }
            //}

            return true;
        }
        private void SetStyle1()
        {
            LinearGradientBrush linearGradientBrush;
            System.Windows.Style style;
            ControlTemplate controlTemplate;
            FrameworkElementFactory border;
            FrameworkElementFactory contentPresenter;
            Trigger trigger;

            linearGradientBrush = new LinearGradientBrush("#FF5BB75B".ToMediaColor(), "#FF398239".ToMediaColor(), new Point(0.5, 1), new Point(0.5, 0));


            style = new System.Windows.Style(typeof(Button));
            style.Setters.Add(new Setter(Button.FontSizeProperty, 11D));
            style.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.DarkGreen));


            controlTemplate = new ControlTemplate(typeof(Button));


            border = new FrameworkElementFactory(typeof(Border));
            border.Name = "ButtonBorder";
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(10));
            border.SetValue(Border.BorderBrushProperty, new SolidColorBrush("#387f38".ToMediaColor()));
            border.SetValue(Border.PaddingProperty, new Thickness(0));
            border.SetValue(Border.BackgroundProperty, new LinearGradientBrush("#FF5BB75B".ToMediaColor(), "#FF449B44".ToMediaColor(), new Point(0.5, 1), new Point(0.5, 0)));


            contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.Name = "ButtonContentPresenter";
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            border.AppendChild(contentPresenter);


            trigger = new Trigger();
            trigger.Property = Button.IsMouseOverProperty;
            trigger.Value = true;
            trigger.Setters.Add(new Setter()
            {
                TargetName = "ButtonBorder",
                Property = Border.BackgroundProperty,
                Value = linearGradientBrush
            });

            trigger.Setters.Add(new Setter()
            {
                Property = Border.CursorProperty,
                Value = Cursors.Hand
            }
            );

            controlTemplate.VisualTree = border;
            controlTemplate.Triggers.Add(trigger);

            this.ResourceDictionaryRoot.Add(typeof(Button), style);
        }
        private void SetStyle3()
        {
            ControlTemplate controlTemplate;
            FrameworkElementFactory frameworkElementFactory;
            System.Windows.Style style;
            Trigger trigger;
            Setter setter;
            Setter se1;

            controlTemplate = new ControlTemplate(typeof(Button));

            frameworkElementFactory = new FrameworkElementFactory(typeof(Border));
            frameworkElementFactory.Name = "Border";
            frameworkElementFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            frameworkElementFactory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            controlTemplate.VisualTree = frameworkElementFactory;

            style = new System.Windows.Style(typeof(Button));
            style.Setters.Add(new Setter(Button.OverridesDefaultStyleProperty, true));

            se1 = new Setter();
            //se1.Property = System.Windows.Controls.FrameworkElement.TemplateProperty;


            trigger = new Trigger();
            trigger.Property = Button.IsMouseOverProperty;
            trigger.Value = true;

            setter = new Setter();
            setter.TargetName = "Border";
            setter.Property = Button.BackgroundProperty;
            setter.Value = new SolidColorBrush(Color.FromRgb(255, 0, 0));

            trigger.Setters.Add(setter);

            style.Triggers.Add(trigger);

            style.Setters.Add(se1);

            this.ResourceDictionaryRoot.Add(typeof(Button), style);
        }
        private void SetStyle2()
        {
            ControlTemplate controlTemplate;
            FrameworkElementFactory frameworkElementFactory;
            System.Windows.Style style;
            Trigger trigger;
            Setter setter;

            controlTemplate = new ControlTemplate(typeof(Button));

            frameworkElementFactory = new FrameworkElementFactory(typeof(Border));
            frameworkElementFactory.Name = "Border";
            frameworkElementFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            frameworkElementFactory.SetValue(Border.BorderBrushProperty, Brushes.Black);
            controlTemplate.VisualTree = frameworkElementFactory;

            style = new System.Windows.Style(typeof(Button));

            style.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Red));
            style.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Green));

            trigger = new Trigger();
            trigger.Property = Button.IsMouseOverProperty;
            trigger.Value = true;

            setter = new Setter();
            setter.TargetName = "Border";
            setter.Property = Button.BackgroundProperty;
            setter.Value = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            trigger.Setters.Add(setter);

            //trigger.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush(Color.FromRgb(255, 0, 0)), "Border"));

            style.Triggers.Add(trigger);

            this.ResourceDictionaryRoot.Add(typeof(Button), style);
        }
        #endregion
    }
}
