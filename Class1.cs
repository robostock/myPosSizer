using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CtrlLib;
using WealthLab;
using Fidelity.Components;
using WealthLab.PosSizers;

namespace myPosSizer
{
    public class myPosSizerSettings : WealthLab.PosSizers.PosSizerSettingsBase
    {
    }

    public class myPosSizer : WealthLab.PosSizers.BasicPosSizer, ICustomSettings
    {
        public override string FriendlyName
        {
            get
            {
                return "myPosSizer";
            }
        }

        public override double SizePosition(Position currentPos, Bars bars, int bar, double basisPrice, PositionType pt, double riskStopLevel, double equity, double cash)
        {
            double risksizeprecent = Math.Abs((riskStopLevel - basisPrice) / basisPrice - 1);

            if (_settings == null)
                _settings = new myPosSizerSettings();
            this.InitializeSettings(_settings);
            _maxRisk = _settings.MaxRiskSize;

            //MessageBox.Show("MaxRisk:"+_maxRisk,ToString());
            double capfortrade = equity *0.99*_maxRisk/100;
            capfortrade = capfortrade/Math.Abs(riskStopLevel - basisPrice);
            if (capfortrade > equity)
                capfortrade = equity;
            return (int) (Math.Min(capfortrade,equity*0.99/basisPrice));
        }

        //* members 
        public myPosSizerSettings _settings;
        public double _maxRisk = 1; //риск в процентах
        //ICustomSettings 
        #region ICustomSettings Members 
        public UserControl GetSettingsUI()
        {
            myPosSizerControl ctrl_settings = new myPosSizerControl();
            //MessageBox.Show("GetSettings:"+_maxRisk.ToString());
            
            //ctrl_settings.MaxRisk = _maxRisk;

            if (_settings == null)
                _settings = new myPosSizerSettings();
            _settings.MaxRiskSize = _maxRisk;
            InitializeSettings(_settings);
            //MessageBox.Show("GetSettings 2:" + _settings.MaxRiskSize.ToString());

            _maxRisk = _settings.MaxRiskSize;
            ctrl_settings.MaxRisk = _maxRisk;

            return ctrl_settings;
        }
        public void ChangeSettings(UserControl ui)
        {
            myPosSizerControl ctrl_settings = ui as myPosSizerControl;

            //MessageBox.Show("ChangeSettings:"+_maxRisk.ToString());

            _maxRisk = ctrl_settings.MaxRisk;
            //MessageBox.Show("ChangeSettings2:" + _maxRisk.ToString());
            
            
            _settings.MaxRiskSize = _maxRisk;
            //ChangeBasicSettings(_settings);

            //_maxRisk = _settings.MaxRiskSize;
            //ChangeBasicSettings(_settings);
        }
        
        public void ReadSettings(ISettingsHost host)
        {
            _maxRisk = host.Get("myPosSizer._maxRisk", (double)0);
            //MessageBox.Show("ReadSettings:"+_maxRisk.ToString());

            ReadBasicSettings(host);
            _maxRisk = host.Get("myPosSizer._maxRisk", (double)0);
            //MessageBox.Show("ReadSettings 2:" + _maxRisk.ToString());
        }
        public void WriteSettings(ISettingsHost host)
        {
            host.Set("myPosSizer._maxRisk", _maxRisk);
            //MessageBox.Show("WriteSettings:" + _maxRisk.ToString());

            WriteBasicSettings(host);
        }
        #endregion//*/
    }
}
