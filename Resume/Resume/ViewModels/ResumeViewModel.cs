﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Resume.ViewModels
{
    public class ResumeViewModel
    {
        public string HighlightsTitle1 { get; set; } = "Mobile Developer";
        public string HighlightsBlurb1 { get; set; } = @"• testing this bullet
• testing this bullet as well
• lets also test this one";

        public string HighlightsTitle2 { get; set; } = "Engineer";
        public string HighlightsBlurb2 { get; set; } = @"• testing more bullets
• continuing the test
• hello i am computer";
    }
}
