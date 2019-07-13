//using System.Threading.Tasks;
//using Windows.ApplicationModel.Background;
//using BrickPi3.Sensors;
//using BrickPi3.Models;
//using System.Diagnostics;
//using BrickPi3;

//namespace BrickPi3Testing
//{
//    public class LightSensor : IBackgroundTask
//    {
//        private Brick Brick { get; set; }

//        public LightSensor(Brick brick)
//        {
//            Brick = brick;
//        }

//        public async Task TestNXTLightSensor()
//        {
//            NXTLightSensor light = new NXTLightSensor(Brick, BrickPortSensor.PORT_S3, LightMode.Ambient, 50);
//            int count = 0;
//            while (count < 400)
//            {
//                Debug.WriteLine(string.Format($"NXT Light value: {light.Value}"));
//                await Task.Delay(100);
//                count++;
//            }
//        }
//    }
//}
