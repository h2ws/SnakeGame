using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    //没有碰撞检测的方块蛇, 
    class Snake
    {
        public enum Diretion{
            up , left, down, right
        }
        
        public class Node : ICloneable{
            public Node Next;
            public Point Position = new Point(0, 0);

            public Object Clone()
            {
                Node node = new Node();
                node.Position.X = this.Position.X;
                node.Position.Y = this.Position.Y;
                return node;
            }
        }

        private Diretion _headDiretion;
        private Node _headNode = new Node();
        private Graphics _graphics;
        private int _width;
        private int _height;
        private Color _colorBody;

        private int _size;

        public Node HeadNode
        {
            get
            {
                return this._headNode;
            }

        }

        public Diretion HeadDiretion {
            get
            {
                return _headDiretion;
            }
            set
            {
                _headDiretion = value;
            }
        }

        public Color BodyColor
        {
            get
            {
                return _colorBody;
            }

            set
            {
                _colorBody = value;
            }
        }

        public void Elongate()
        {
            Node newNode = (Node)this.HeadNode.Clone();
            newNode.Next = this.HeadNode.Next;
            this.HeadNode.Next = newNode;
        }

        public Snake(Graphics graphics, int size, int width, int height, Color bodyColor)
        {

            this._headDiretion = Diretion.right;

            this._size = size;

            this._graphics = graphics;

            this._headNode.Position = new Point(0, 0);

            this._width = width;
            this._height = height;

            this.BodyColor = bodyColor;
        }

        private void DelLastNode()
        {
            Node p = this._headNode;

            if(p.Next == null)
            {
                throw new Exception("只有一个头结点, 删除尾节点无效");
            }

            while(p.Next.Next != null)
            {
                p = p.Next;
            }

            p.Next = null;

        }

        public void Update()
        {
            //移动头
            if(this._headNode.Next != null)
            {
                this.DelLastNode();
            }

            Node newNode = (Node)this.HeadNode.Clone();

            switch (this.HeadDiretion)
            {
                case Diretion.down:
                    this._headNode.Position.Y += this._size;
                    break;
                case Diretion.up:
                    this._headNode.Position.Y -= this._size;
                    break;
                case Diretion.left:
                    this._headNode.Position.X -= this._size;
                    break;
                case Diretion.right:
                    this._headNode.Position.X += this._size;
                    break;
            }
            newNode.Next = this._headNode.Next;
            this._headNode.Next = newNode;

            Brush blackBrush = new SolidBrush(Color.Black);

            //清空画板
            Brush bgBrush = new SolidBrush(Color.White);
            Brush bodyBrush = new SolidBrush(this.BodyColor);
            this._graphics.FillRectangle(bgBrush, 0, 0, this._width, this._height);

            //绘制头
            Node p = this._headNode.Next;
            this._graphics.FillRectangle(
                 blackBrush,
                this.HeadNode.Position.X,
                this.HeadNode.Position.Y,
                this._size,
                this._size
                );

            //绘制身体
            while(p != null)
            {
                DrawNode(p);
                p = p.Next;
            }
        }

        //绘制方块节点, 颜色为BodyColor
        //待改为抽象方法
        private void DrawNode(Node node)
        {
            Brush bodyBrush = new SolidBrush(this.BodyColor);
            this._graphics.FillRectangle(
                 bodyBrush,
                node.Position.X,
                node.Position.Y,
                this._size,
                this._size
                );

        }

    }

}
