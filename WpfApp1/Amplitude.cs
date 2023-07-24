using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Amplitude : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Текущее значение свойства Value
        /// </summary>
        private double amplitudePropertyValue;

        #endregion

        #region Properties

        /// <summary>
        /// Текущее значение амплитуды
        /// </summary>
        public double Value
        {
            get { return amplitudePropertyValue; }
            set
            {
                if (amplitudePropertyValue != value)
                {
                    amplitudePropertyValue = value;
                    OnPropertyChanged("Amplitude");
                }
            }
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Private methods

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
