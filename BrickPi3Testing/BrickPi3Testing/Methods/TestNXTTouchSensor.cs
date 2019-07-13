//using System.Threading.Tasks;
//using Windows.ApplicationModel.Background;
//using BrickPi3.Sensors;
//using BrickPi3.Models;
//using System.Diagnostics;
//using System.ComponentModel;
//using System;
//using BrickPi3;

//namespace BrickPi3Testing
//{
//    public sealed partial class StartupTask : IBackgroundTask
//    { 
//        //private NXTTouchSensor TouchSensor { get; set; }
//        //private Brick Brick { get; set; }

//        public void TestTouchSensor()
//        {
            
//            //NXTTouchSensor touch = new NXTTouchSensor(Brick, BrickPortSensor.PORT_S1);
//            //int count = 0;
//            //while (count < 400)
//            //{
//            //    Debug.WriteLine(string.Format($"NXT Touch sensor: {touch.IsPressed()}"));
//            //    Task.Delay(100);
//            //    count++;
//            //}

//            var touchSensor = new NXTTouchSensor(Brick, BrickPortSensor.PORT_S1, timeout: 100);
//            touchSensor.PropertyChanged += new PropertyChangedEventHandler(OutputTouchSensorStatus);
//        }

//        private void OutputTouchSensorStatus(object sender, PropertyChangedEventArgs e)
//        {
//            Debug.WriteLine(string.Format($"NXT Touch sensor property changed: {e.PropertyName}"));
//        }
//    }
//}
