﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Employr.ViewModels;
using Xamarin.Forms;

namespace Employr.Views
{
    public partial class HighlightsView : ContentPage, IIntroAnimation
    {

        private bool _appeared = false;

        public HighlightsView()
        {
            BindingContext = new HighlightsViewModel();
            InitializeComponent();

            _scrollView.SetBinding(View.MarginProperty, new Binding("Height", source: _nameStack, converter: new ThicknessConverter(),
                converterParameter: 1, mode: BindingMode.OneWay));

            _highlightsStack.SetBinding(View.MarginProperty, new Binding("Height", source: _backgroundImage, converter: new ThicknessConverter(),
                converterParameter: 1, mode: BindingMode.OneWay));

            _nameStack.SetBinding(HeightRequestProperty, new Binding("Height", source: _resumeViewFAB));

            AnimationUtilities.InitializeAppear(_title1, _body1, _title2, _body2);
        }
        //todo: math to stop scrolling
        public void OnScrolled(object sender, ScrolledEventArgs e)
        {
            //scale FAB
            var imageHeight = _backgroundImage.Height;
            var startY = _resumeViewFAB.Y;
            double imageMotion;

            imageMotion = Math.Max(-e.ScrollY * (startY / imageHeight), -startY);
            _resumeViewFAB.TranslationY = imageMotion;

            var progressImage = -imageMotion / startY;
            _resumeViewFAB.Scale = 1 - .2 * progressImage;

            //scale and fade _nameStack
            var stackTravelDistance = _nameStack.Y;
            var fadeOffset = _resumeViewFAB.Height / (2 * stackTravelDistance);
            double stackMotion;

            stackMotion = Math.Max(-e.ScrollY, -stackTravelDistance);
            _nameStack.TranslationY = stackMotion;

            if (stackMotion == 0)
            {
                _nameStack.BackgroundColor = Color.FromHex("#A5A5A5");
                return;
            }

            var progressStack = Math.Max(1 + fadeOffset + (stackMotion / stackTravelDistance) / 2, .5);
            var color = _nameStack.BackgroundColor;
            Color newColor = new Color(color.R, color.G, color.B, progressStack);
            _nameStack.BackgroundColor = newColor;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            OnScrolled(this, new ScrolledEventArgs(0, 0));
            _scrollView.ScrollToAsync(0, 0, false);
        }

        public void HandleResumeButtonPressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomWebView());
        }

        public void HandleCodeViewButtonPressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CodeView(new Uri("http://i.imgur.com/dEuxzcH.png")));
        }

        public void RunIntroAnimations()
        {
            if (!_appeared)
            {
                AnimationUtilities.FeatherIn(_title1, _body1, _title2, _body2);
                _appeared = true;
            }
        }
    }
}
