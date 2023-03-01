using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuteCoolTrackBar
{
    public partial class CustomTrackBar : Control
    {
        #region Misc Proccess
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Invalidate();
            }
        }
        #endregion

        #region Initial Processing
        public CustomTrackBar()
        {
            DoubleBuffered = true;//ちらつき嫌いだから
            this.Size = _defaultsize;
            GetValue();

        }
        #endregion

        #region variable
        public enum MemoryStates
        {
            Up,
            Under,
            Both
        };
        public enum StringStates
        {
            Up,
            Under
        };
        public enum AddCharStates
        {
            Before,
            After,
            None
        };
        //内部用
        //現在のthumb位置を保存するためのもの
        bool _thumbClicked = false;
        Rectangle ThumbRect = new Rectangle(0, 0, 1, 1);
        float left;
        float right;
        //指定の数に習って1当たりのlocを使って指定した数ごとに縦線を引く
        int tickcount;//必要tick数
        float processwidth;
        float perwidth;
        float loc;


        //Margin用
        private int _rightblank = 10;
        private int _leftblank = 10;
        //初期変数
        private Size _defaultsize = new Size(440, 70);
        //基本
        private int _value = 5;
        private int _maximum = 9;
        private int _minimum = 1;
        private int _largechange = 1;

        //トラック
        private int _trackheight = 15;
        private Point _trackoffset = new Point(0, 0);
        private int _trackradius = 1;
        private Color _beforetrackcolor = Color.Red;
        private Color _aftertrackcolor = Color.Gray;

        //つまみ
        private Size _thumbsize = new Size(16, 30);
        private Point _thumboffset = new Point(1, 0);
        private int _thumbradius = 3;//丸み
        private Color _thumbcolor = Color.White;

        //メモリ
        private Size _memorysize = new Size(5, 25);
        private Point _memoryoffset = new Point(0, 0);
        private int _memoryradius = 0;
        private Color _memorycolor = Color.Gray;
        private bool _memoryvisible = true;
        private int _memoryfrequency = 1;
        private MemoryStates _memorystate = MemoryStates.Both;
        private int _horizontalblank = 12;


        //文字
        private Font _stringfont = new Font("Meiryo UI", 9);
        private Color _stringcolor = Color.White;
        private Point _stringoffset = new Point(0, 0);
        private bool _stringvisible = true;
        private int _stringfrequency = 1;
        private StringStates _stringstate = StringStates.Up;
        private string _addstring;
        private AddCharStates _addcharstate = AddCharStates.None;



        [Browsable(true)]
        [Category("CustomBlank")]
        [Description("The left value.")]
        public int LeftBlank
        {
            get { return _leftblank; }
            set
            {
                _leftblank = value;

            }
        }

        [Browsable(true)]
        [Category("CustomBlank")]
        [Description("The right value.")]
        public int RightBlank
        {
            get { return _rightblank; }
            set
            {
                _rightblank = value;

            }
        }


        [Category("General")]
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Invalidate();
            }
        }

        [Category("General")]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                Invalidate();
            }
        }

        [Category("General")]
        public int Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;
                Invalidate();
            }
        }

        [Category("General")]
        public int LargeChange
        {
            get { return _largechange; }
            set
            {
                _largechange = value;
                Invalidate();
            }
        }


        [Category("Track")]
        public int TrackHeight
        {
            get { return _trackheight; }
            set
            {
                _trackheight = value;
                Invalidate();
            }
        }

        [Category("Track")]
        public Point TrackOffset
        {
            get { return _trackoffset; }
            set
            {
                _trackoffset = value;
                Invalidate();
            }
        }

        [Category("Track")]
        public int TrackRadius
        {
            get { return _trackradius; }
            set
            {
                _trackradius = value;
                Invalidate();
            }
        }

        [Category("Track")]
        public Color BeforeTrackColor
        {
            get { return _beforetrackcolor; }
            set
            {
                _beforetrackcolor = value;
                Invalidate();
            }
        }
        [Category("Track")]
        public Color AfterTrackColor
        {
            get { return _aftertrackcolor; }
            set
            {
                _aftertrackcolor = value;
                Invalidate();
            }
        }

        [Category("Thumb")]
        public Size ThumbSize
        {
            get { return _thumbsize; }
            set
            {
                _thumbsize = value;
                Invalidate();
            }
        }

        [Category("Thumb")]
        public Point ThumbOffset
        {
            get { return _thumboffset; }
            set
            {
                _thumboffset = value;
                Invalidate();
            }
        }

        [Category("Thumb")]
        public int ThumbRadius
        {
            get { return _thumbradius; }
            set
            {
                _thumbradius = value;
                Invalidate();
            }
        }

        [Category("Thumb")]
        public Color ThumbColor
        {
            get { return _thumbcolor; }
            set
            {
                _thumbcolor = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public Size MemorySize
        {
            get { return _memorysize; }
            set
            {
                _memorysize = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public Point MemoryOffset
        {
            get { return _memoryoffset; }
            set
            {
                _memoryoffset = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public int MemoryRadius
        {
            get { return _memoryradius; }
            set
            {
                _memoryradius = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public Color MemoryColor
        {
            get { return _memorycolor; }
            set
            {
                _memorycolor = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public bool MemoryVisible
        {
            get { return _memoryvisible; }
            set
            {
                _memoryvisible = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public int MemoryFrequency
        {
            get { return _memoryfrequency; }
            set
            {
                _memoryfrequency = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public MemoryStates MemoryState
        {
            get { return _memorystate; }
            set
            {
                _memorystate = value;
                Invalidate();
            }
        }

        [Category("Memory")]
        public int HorizontalBlank
        {
            get { return _horizontalblank; }
            set
            {
                _horizontalblank = value;
                Invalidate();
            }
        }

        [Category("String")]
        public Font StringFont
        {
            get { return _stringfont; }
            set
            {
                _stringfont = value;
                Invalidate();
            }
        }

        [Category("String")]
        public Color StringColor
        {
            get { return _stringcolor; }
            set
            {
                _stringcolor = value;
                Invalidate();
            }
        }

        [Category("String")]
        public Point StringOffset
        {
            get { return _stringoffset; }
            set
            {
                _stringoffset = value;
                Invalidate();
            }
        }

        [Category("String")]
        public bool StringVisible
        {
            get { return _stringvisible; }
            set
            {
                _stringvisible = value;
                Invalidate();
            }
        }

        [Category("String")]
        public int StringFrequency
        {
            get { return _stringfrequency; }
            set
            {
                _stringfrequency = value;
                Invalidate();
            }
        }

        [Category("String")]
        public StringStates StringState
        {
            get { return _stringstate; }
            set
            {
                _stringstate = value;
                Invalidate();
            }
        }

        [Category("String")]
        public string AddString
        {
            get { return _addstring; }
            set
            {
                _addstring = value;
                Invalidate();
            }
        }

        [Category("String")]
        public AddCharStates AddCharState
        {
            get { return _addcharstate; }
            set
            {
                _addcharstate = value;
                Invalidate();
            }
        }
        #endregion

        #region DrawGraphics

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            GetValue();
            DrawNumber(e.Graphics);
            DrawMemory(e.Graphics);
            DrawTrack(e.Graphics);
            DrawThumb(e.Graphics);

        }

        private void GetValue()
        {
            left = LeftBlank + HorizontalBlank - MemorySize.Width + (MemorySize.Width / 2);
            right = this.Width - (RightBlank + HorizontalBlank) + MemorySize.Width - (MemorySize.Width / 2) - 1;
            //指定の数に習って1当たりのlocを使って指定した数ごとに縦線を引く
            tickcount = Maximum - Minimum + Minimum;//必要tick数
            processwidth = right - left;
            perwidth = (float)processwidth / ((float)tickcount - Minimum);
            loc = left + ((Value) * perwidth);
            ThumbRect = new Rectangle((int)(loc - ThumbSize.Width / 2) + ThumbOffset.X, this.Height / 2 - ThumbSize.Height / 2 + ThumbOffset.Y, ThumbSize.Width, ThumbSize.Height);

        }

        private void DrawTrack(Graphics g)
        {
            int width = this.Width - (LeftBlank + RightBlank);
            int height = TrackHeight;
            if (TrackRadius == 0)
            {
                //前と後ろの二つを描画するように変更する
                g.FillRectangle(new SolidBrush(BeforeTrackColor), new Rectangle(LeftBlank, (this.Height / 2) - (TrackHeight / 2), ThumbRect.Left + ThumbRect.Width / 2, height));
                g.FillRectangle(new SolidBrush(AfterTrackColor), new Rectangle(ThumbRect.Left + ThumbRect.Width / 2, (this.Height / 2) - (TrackHeight / 2), this.Width - RightBlank - (ThumbRect.Left + ThumbRect.Width / 2), height));

            }
            else
            {
                DrawRoundRect(g, new Rectangle(LeftBlank, (this.Height / 2) - (TrackHeight / 2), ThumbRect.Left + ThumbRect.Width / 2 - LeftBlank, height), BeforeTrackColor, TrackRadius);
                DrawRoundRect(g, new Rectangle(ThumbRect.Left + ThumbRect.Width / 2, (this.Height / 2) - (TrackHeight / 2), (this.Width - RightBlank) - (ThumbRect.Left + ThumbRect.Width / 2), height), AfterTrackColor, TrackRadius);

            }

        }

        private void DrawThumb(Graphics g)
        {

            if (ThumbRadius != 0)
                DrawRoundRect(g, new Rectangle((int)(loc - ThumbSize.Width / 2) + ThumbOffset.X, this.Height / 2 - ThumbSize.Height / 2 + ThumbOffset.Y, ThumbSize.Width, ThumbSize.Height), ThumbColor, ThumbRadius);
            else
                g.FillRectangle(new SolidBrush(ThumbColor), new Rectangle((int)(loc - ThumbSize.Width / 2) + ThumbOffset.X, this.Height / 2 - ThumbSize.Height / 2 + ThumbOffset.Y, ThumbSize.Width, ThumbSize.Height));
        }
        //ThumbとMemoryで同じような処理しているので時間があれば綺麗にする
        private void DrawMemory(Graphics g)
        {
            int a = 0;
            for (int i = Minimum; i < Maximum + 1; i++)
            {
                if (i == Minimum || i == Maximum + 1 || i % StringFrequency == 0)
                {
                    float loc = left + (a * perwidth);
                    g.FillRectangle(new SolidBrush(MemoryColor), new RectangleF(loc - ThumbSize.Width / 2 + ThumbSize.Width / 4, this.Height / 2 - MemorySize.Height / 2, MemorySize.Width, MemorySize.Height));

                }
                a++;
            }

        }

        private void DrawNumber(Graphics g)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            switch ((int)StringState)
            {
                case 0:
                    format.LineAlignment = StringAlignment.Near;
                    break;
                case 1:
                    format.LineAlignment = StringAlignment.Far;
                    break;
            }
            int a = 0;
            for (int i = Minimum; i < Maximum + 1; i++)
            {
                if (i == Minimum || i == Maximum + 1 || i % StringFrequency == 0)
                {
                    float loc = left + (a * perwidth);
                    RectangleF recttest = new RectangleF(loc - MemorySize.Width / 2 - 20 + StringOffset.X, 0, 40, this.Height);//1...15 = width/2

                    int rectwidth = 40;
                    RectangleF rectf = new RectangleF(loc - ThumbSize.Width / 2 + ThumbSize.Width / 4 - rectwidth / 2 + rectwidth / 8, 0, rectwidth, this.Height);//1...15 = width/2
                    //g.FillRectangle(new SolidBrush(Color.Red), rectf);
                    g.DrawString(i + AddString, StringFont, new SolidBrush(StringColor), rectf, format);
                }
                a++;
            }
        }



        private void DrawRoundRect(Graphics g, Rectangle rect, Color color, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();

            // 左上の角丸
            path.AddArc(rect.Left, rect.Top,
                radius * 2, radius * 2,
                180, 90);
            // 上の線
            path.AddLine(rect.Left + radius, rect.Top,
                rect.Right - radius, rect.Top);
            // 右上の角丸
            path.AddArc(rect.Right - radius * 2, rect.Top,
                radius * 2, radius * 2,
                270, 90);
            // 右の線
            path.AddLine(rect.Right, rect.Top + radius,
                rect.Right, rect.Bottom - radius);
            // 右下の角丸
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2,
                radius * 2, radius * 2,
                0, 90);
            // 下の線
            path.AddLine(rect.Right - radius, rect.Bottom,
                rect.Left + radius, rect.Bottom);
            // 左下の角丸
            path.AddArc(rect.Left, rect.Bottom - radius * 2,
                radius * 2, radius * 2,
                90, 90);
            // 左の線
            path.AddLine(rect.Left, rect.Bottom - radius,
                rect.Left, rect.Top + radius);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillPath(new SolidBrush(color), path);
            g.SmoothingMode = SmoothingMode.Default;
            path.CloseFigure();

        }

        #endregion

        #region MouseEvent
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                _thumbClicked = ThumbHit(e.Location);
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _thumbClicked = false;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_thumbClicked)
            {
                //クリックした位置を確認、そこから一番近いValueに変更する
                int calculationValue = (int)(Math.Round((e.Location.X - (LeftBlank + HorizontalBlank)) / perwidth));
                if (calculationValue < 0)
                    calculationValue = Minimum;
                if (calculationValue > Maximum - (Minimum))
                    calculationValue = Maximum - (Minimum);
                Value = calculationValue;
            }
        }



        private bool ThumbHit(Point location)
        {
            return ThumbRect.Contains(location);
        }
        #endregion
    }
}
