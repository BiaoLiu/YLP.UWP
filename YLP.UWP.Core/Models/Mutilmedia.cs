using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Common;

namespace YLP.UWP.Core.Models
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class mutilmedia
    {
        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("image", typeof(mutilmediaLabel))]
        [System.Xml.Serialization.XmlElementAttribute("label", typeof(mutilmediaLabel))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class mutilmediaLabel : BindableBase
    {

        private string textField;

        private bool boldField;

        private int sizeField;

        private string colorField;

        private string aligmentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string text
        {
            get
            {
                return this.textField;
            }
            set
            {
                SetProperty(ref textField, value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool bold
        {
            get
            {
                return this.boldField;
            }
            set
            {
                this.boldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string aligment
        {
            get
            {
                return this.aligmentField;
            }
            set
            {
                this.aligmentField = value;
            }
        }

        private string urlField;

        private string onclickField;

        private string mimeField;

        private int widthField;

        private int heightField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                SetProperty(ref urlField, value);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string onclick
        {
            get
            {
                return this.onclickField;
            }
            set
            {
                this.onclickField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string mime
        {
            get
            {
                return this.mimeField;
            }
            set
            {
                this.mimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
    }
}
