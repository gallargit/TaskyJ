using System.Runtime.Serialization;
using TaskyJ.Globals.Data.Helpers;

namespace TaskyJ.Globals.Data.DataObjects
{
    public class DBCategoryJ : BaseEntity
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string IconBase64
        {
            get;
            set;
        }

        public TaskyJImage Icon
        {
            get
            {
                return ImageHelper.Base64StringToImage(IconBase64);
            }
            set
            {
                IconBase64 = ImageHelper.ImageToBase64(value);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(BaseEntity source)
        {
            if (source is DBCategoryJ sourcej)
            {
                return Name == sourcej.Name && base.Equals(source);
            }
            else
            {
                return base.Equals(source);
            }
        }
    }
}
