using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace WpfApp1
{
    class HarmonicSeries : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// �������� �������� Amplitudes.
        /// </summary>
        private Amplitude[] amplitudeValues;

        /// <summary>
        /// �������� �������� OutputAmplitude.
        /// </summary>
        private double[] outputValues;

        #endregion

        #region Constructor

        /// <summary>
        /// ������� ����� ��������� ������������� �����
        /// </summary>
        /// <param name="numberOfHarmonics">���������� ������������ ��������
        /// � �������� Amplitudes</param>
        /// <param name="period">���������� ����� ��� �������� �������� ��������</param>
        public HarmonicSeries(int numberOfHarmonics, int period)
        {
            amplitudeValues = new Amplitude[numberOfHarmonics];
            outputValues = new double[period];

            for (int i = 0; i < amplitudeValues.Length; ++i)
            {
                amplitudeValues[i] = new Amplitude();
                amplitudeValues[i].PropertyChanged += new PropertyChangedEventHandler(HarmonicSeries_PropertyChanged);
             }
            amplitudeValues[0].Value = 1.0;
        }

        #endregion

        #region Change handling

        /// <summary>
        /// ���������� ���������� ������� Amplitudes, ����� ��������� ��� � ���, ���
        /// �������� ��������� ����������
        /// </summary>
        void HarmonicSeries_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Amplitudes");
            UpdateValues();
        }

        #endregion

        #region Properties

        //��������� �������� � ����
        public Amplitude[] Amplitudes
        {
            get { return amplitudeValues; }
        }

        //�������������� ��������� ��������
        public double[] Output
        {
            get { return outputValues; }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Private methods

        /// <summary>
        /// �������� �������� ������ �� ������ ������� ��������
        /// </summary>
        private void UpdateValues()
        {
            for (int point = 0; point < outputValues.Length; ++point)
            {
                double theta = point * 2 * Math.PI;
                theta /= outputValues.Length;

                double value = 0;
                for (int harmonic = 0; harmonic < amplitudeValues.Length; ++harmonic)
                {
                    value += amplitudeValues[harmonic].Value * Math.Sin(theta * (harmonic + 1));
                }

                outputValues[point] = value;
            }
        }

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
