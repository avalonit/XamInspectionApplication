using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KobApplication.Controls
{
    public class MyEntry : Entry
    {
        public Color CustomBackgroundColor { get; set; } = Color.Transparent;
        public Thickness CustomPadding { get; internal set; }
    }
}
