using System.Windows.Forms;
using System.Drawing;
using System;
public class ColorButton : Button
{
    public ColorButton()
    {
        Text = "";
    }

    public event VoidColor ColorChanged;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnClick(System.EventArgs e)
    {
        base.OnClick(e);

        ColorDialog cd = new ColorDialog();
        cd.SolidColorOnly = true;
        if (cd.ShowDialog() == DialogResult.OK)
        {
            SelectedColor = cd.Color;
        }
    }

    Color textColor
    {
        set
        {
            ForeColor = value;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Color SelectedColor
    {
        get
        {
            return BackColor;
        }
        set
        {
            if (ColorChanged != null)
            {
                ColorChanged(value);   
            }
            BackColor = value;
        }
    }

    /// <summary>
    /// From a given colour it works out a suitable colour that will sit on top of
    /// it so that the contrast is suitable for readability.
    /// </summary>
    /// <param name="baseColor">Color to get the contrasting complement of</param>
    /// <returns>Contrasting color</returns>
}
