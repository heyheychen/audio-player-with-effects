using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace audioplayer_with_EQ_MBDRC
{
    class trackbar_no_focus_cue: System.Windows.Forms.TrackBar
    {

        protected override void OnEnter(EventArgs e)
        {
           // base.OnEnter(e);
        }
        protected override void OnChangeUICues(UICuesEventArgs e)
        {
            //base.OnChangeUICues(e);
        }
    
    }
}
