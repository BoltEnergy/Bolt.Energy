using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace Com.Comm100.Forum.UI.Images
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.BufferOutput = true;
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.UtcNow.AddMinutes(-1);
            Response.CacheControl = "no-cache";

            try
            {
                string tmpcode = RndCode(4);
                Response.Cookies["code"].Value = tmpcode;
                HttpCookie a = new HttpCookie("ImageV",tmpcode);
                Response.Cookies.Add(a);
                this.ValidateCode(tmpcode);
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }

        private void ValidateCode(string vnum)
        {
            Bitmap img = null;
            Graphics g = null;
            MemoryStream ms = null;

            int gheight = vnum.Length * 18;
            img = new Bitmap(gheight, 30);
            g = Graphics.FromImage(img);
            System.Random random = new Random();

            g.Clear(Color.White);

            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(img.Width);
                int x2 = random.Next(img.Width);
                int y1 = random.Next(img.Height);
                int y2 = random.Next(img.Height);
                g.DrawLine(new Pen(Color.Silver),x1,x2,y1,y2);
            }

            Font f = new Font("Arial Black", 15);
            SolidBrush s = new SolidBrush(Color.Black);
            g.DrawString(vnum, f, s, 3, 3);

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(img.Width);
                int y = random.Next(img.Height);

                img.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());

            g.Dispose();
            img.Dispose();

            Response.End();
        }

        public static string RndCode(int vcodenum)
        {
            string vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p" +
                ",q,r,s,t,u,v,w,x,y,z";
            string[] vcarray = vchar.Split(new Char[] { ',' });
            string vcode = "";
            int temp = -1;

            Random rand = new Random();

            for (int i = 1; i < vcodenum + 1; i++)
            {
                if (temp != -1)
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));

                int t = rand.Next(35);
                if (temp != -1 && temp == t)
                    return RndCode(vcodenum);

                temp = t;
                vcode += vcarray[t];
            }
            
            return vcode;
        }

    }
}
