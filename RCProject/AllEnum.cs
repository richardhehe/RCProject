using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject
{
    public enum machine
    {
        Null = 0,
        READY = 1,
        RUNNING = 2,
        RESETING = 3,
        ALARMED = 4,
        EMERGENCY = 5,
    }

    public enum HadewareStatus
    {
        Laser = 0,
        Camera,
        PLC,
        Power,
        Laser_Null,
        Camera_Null,
        PLC_Null,
        Power_Null
    }

    public enum RecipeEnum
    {
        PrismaticBattery,
    }
    public enum configuration
    {
        FrontTakePhoto=1,
FrontPhotoResult,
FrontTakePhotoPowerFront,
FrontTakePhotoPowerBack,
PhotoPowerFrontDelay,
TakePhotoPowerBackDelay,
CircleFrontTakePhoto,
CircleFrontTakePhotoPowerFront,
CircleFrontTakePhotoPowerBack,
CircleFrontPhotoPowerDelay,
CircleRightTakePhoto,
CircleRightTakePhotoPowerFront,
CircleRightTakePhotoPowerBack,
CircleRightPhotoPowerDelay,
CircleBackTakePhoto,
CircleBackTakePhotoPowerFront,
CircleBackTakePhotoPowerBack,
CircleBackPhotoPowerDelay,
CircleLeftTakePhotoResult,
CircleLeftTakePhotoPowerFront,
CircleLeftTakePhotoPowerBack,
CircleLeftPhotoPowerDelay,
 RecheckBackTakePhoto,
RecheckBackTakePhotoPowerFront,
RecheckBackTakePhotoPowerBack,
RecheckBackPhotoPowerDelay,


    }

    public enum TestStatus
     {
        Nul=0,
        FrontLaserFinsh, 
        FrontCameraFinsh,
        BackLaserFinish,
        BackCameraFinish,
     }


    public enum NinePoint
    {
        Point1=1,//Point0对应9点当中的第一个点
        Point2,
        Point3,
        Point4,
        Point5,
        Point6,
        Point7,
        Point8,
        Point9,       
    }


    public enum TestFai
    {
        FAI1_A = 0,
        FAI3_C,
        FAI4_D,
        FAI5_E,
        FAI2_B,
        FAI6_F,
        FAI7_G,
        FAI8_H,
        FAI9_I,
        FAI11_K,
        FAI10_J,
        FAI12_L_A,
        FAI12_L_B,
        FAI12_L_C,
        FAI12_L_D,
        FAI12_L_E,
        FAI12_L_F,
        FAI12_L_G,
        FAI12_L_H,
        FAI13_M_A1,
        FAI13_M_B1,
        FAI13_M_C1,
        FAI13_M_D1,
        FAI13_M_E1,
        FAI13_M_F1,
        FAI13_M_G1,
        FAI13_M_H1,
        FAI13_M_A2,
        FAI13_M_B2,
        FAI13_M_C2,
        FAI13_M_D2,
        FAI13_M_E2,
        FAI13_M_F2,
        FAI13_M_G2,
        FAI13_M_H2,
        FAI14_N_1,
        FAI14_N_2,
        FAI14_N_3,
        FAI15_O_1,
        FAI15_O_2,
        FAI15_O_3,
        FAI15_O_4,
        FAI15_O_5,
        FAI15_O_6,
        FAI15_O_7,
        FAI15_O_8,
        FAI15_O_9,
        FAI15_O_10,
        FAI15_O_11,
        FAI15_O_12,
        FAI15_O_13,
        FAI15_O_14,
        FAI15_O_15,
        FAI15_O_16,
        FAI15_O,
        FAI16_P,
        FAI21_S_A3,
        FAI21_S_B3,
        FAI21_S_C3,
        FAI21_S_D3,
        FAI21_S_E3,
        FAI21_S_G3,
        FAI17_Q,
        FAI20_R,
        FAI22_T,
        FAI23_U,
        FAI24_1,
        FAI24_2,
        FAI24_3,
        FAI24_4,
        FAI24_5,
        FAI24_6,
        FAI24_7,
        FAI24_8,
        FAI25_1,
        FAI25_2,
        FAI26_1,
        FAI26_2,
        FAI27_A,
        FAI27_B,
        FAI27_C,
        FAI27_D,
        FAI27_E,
        FAI27_F,
        FAI27_G,
        FAI27_H,
    }
}
