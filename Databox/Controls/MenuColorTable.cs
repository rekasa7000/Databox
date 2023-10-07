using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Databox.Controls
{
    public class MenuColorTable : ProfessionalColorTable
    {
        private Color backColor;
        private Color leftColumnColor;
        private Color borderColor;
        private Color menuItemBorderColor;
        private Color menuItemSelectedColor;

        public MenuColorTable(bool isMainMenu, Color primaryColor)
        {
            if (isMainMenu)
            {
                backColor = Color.FromArgb(63, 63, 63);
                leftColumnColor = Color.FromArgb(40, 40, 40);
                borderColor = Color.FromArgb(25, 25, 25);
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
            else
            {
                backColor = Color.White;
                leftColumnColor = Color.LightGray;
                borderColor = Color.LightGray;
                menuItemBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
        }

        public override Color ToolStripDropDownBackground
        {
            get
            {
                return backColor;
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return borderColor;
            }
        }

        public override Color MenuItemBorder
        {
            get
            {
                return menuItemBorderColor;
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return menuItemSelectedColor;
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return leftColumnColor;
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return leftColumnColor;
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return leftColumnColor;
            }
        }

    }
}
