using AutoMapper;
using System.ComponentModel;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Interface.WPF.Model
{
    public abstract class WPFBaseEntityJ : INotifyPropertyChanged
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public WPFBaseEntityJ()
        {
        }

        public WPFBaseEntityJ(BaseEntity originalObject)
        {
            if (originalObject != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap(originalObject.GetType(), GetType());
                });
                var mapper = new Mapper(config);

                mapper.Map(originalObject, this);
            }
        }

        public BaseEntity ToBaseEntity<T>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(GetType(), typeof(T));
            });

            return config.CreateMapper().Map<T>(this) as BaseEntity;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
