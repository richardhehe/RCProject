using CjBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.NinePointDeal
{
    public class m_RC_Geometry
    {
        public static RC_GeometryTool.Class1 m_RC_Gmy_Class = new RC_GeometryTool.Class1();

        /// <summary>
        /// 校正参数计算函数，注意：1-9个点的顺序必须按照指定顺序排列，9个点拍照坐标，9个点机构坐标，3点算旋转中心，所有数组必须长度对应9/3,所有数组索引从1开始
        /// 校正结果先保存到临时参数变量：m_RC_Gmy_Par_Temp
        /// </summary>
        /// <param name="ccdX">9点校正流程获取到的9个点拍照坐标_X</param>
        /// <param name="ccdY">9点校正流程获取到的9个点拍照坐标_Y</param>
        /// <param name="axisX">9点校正流程机构运动的9个点机构坐标_X</param>
        /// <param name="axisY">9点校正流程机构运动的9个点机构坐标_Y</param>
        /// <param name="circleX">拟圆拍照坐标_X</param>
        /// <param name="circleY">拟圆拍照坐标_Y</param>
        /// <remarks></remarks>
        public static bool m_RC_Gmy_CalFunction_3Point(double[] ccdX, double[] ccdY, double[] axisX, double[] axisY, double[] circleX, double[] circleY, ref string errMsg)
        {

            int[] minX = new int[] { 0, 5, 6, 7, 4, 1, 8 };
            int[] maxX = new int[] { 0, 4, 1, 8, 3, 2, 9 };
            int[] minY = new int[] { 0, 7, 8, 9, 6, 1, 2 };
            int[] maxY = new int[] { 0, 6, 1, 2, 5, 4, 3 };

            int nCx = ccdX.Length;
            int nCy = ccdY.Length;
            int nAx = axisX.Length;
            int nAy = axisY.Length;

            if (nCx < 9)
            {
                LogManager.WriteLog("Point data num  error:  x_CCD data not enough!", LogMode.NinePoint);
                return false;
            }
            if (nCy < 9)
            {
                LogManager.WriteLog("Point data num  error:  y_CCD data not enough!", LogMode.NinePoint);
                return false;
            }
            if (nAx < 9)
            {
                LogManager.WriteLog("Point data num  error:  x_Axis data not enough!", LogMode.NinePoint);
                return false;
            }
            if (nAy < 9)
            {
                LogManager.WriteLog("Point data num  error:  y_Axis data not enough!", LogMode.NinePoint);
                return false;
            }
            if (circleX.Length < 3)
            {
                LogManager.WriteLog("Point data num error:  circleY data num error!", LogMode.NinePoint);
                return false;
            }
            if (circleY.Length < 3)
            {
                LogManager.WriteLog("Point data num error:  circleY data num error!", LogMode.NinePoint);
                return false;
            }
            for (int i = 1; i <= 6; i++)
            {
                if (ccdX[minX[i]] > ccdX[maxX[i]])
                {
                    LogManager.WriteLog("Point data error: x_CCDPoint_" + minX[i].ToString() + "=" + ccdX[minX[i]].ToString() + " // x_CCDPoint_" + maxX[i].ToString() + "=" + ccdX[maxX[i]].ToString(), LogMode.NinePoint);
                    return false;
                }
                if (ccdY[minY[i]] > ccdY[maxY[i]])
                {
                    LogManager.WriteLog("Point data error: y_CCDPoint_" + minY[i].ToString() + "=" + ccdY[minY[i]].ToString() + " // y_CCDPoint_" + maxY[i].ToString() + "=" + ccdY[maxX[i]].ToString(), LogMode.NinePoint);
                    return false;
                }
                if (axisX[minX[i]] > axisX[maxX[i]])
                {
                    LogManager.WriteLog("Point data error: x_axisPoint_" + minX[i].ToString() + "=" + axisX[minX[i]].ToString() + " // x_axisPoint_" + maxX[i].ToString() + "=" + axisX[maxX[i]].ToString(), LogMode.NinePoint);
                    return false;
                }
                if (axisY[minY[i]] > axisY[maxY[i]])
                {
                    LogManager.WriteLog("Point data error: y_axisPoint_" + minY[i].ToString() + "=" + axisY[minY[i]].ToString() + " // y_axisPoint_" + maxY[i].ToString() + "=" + axisY[maxY[i]].ToString(), LogMode.NinePoint);
                    return false;
                }
            }
            bool tResult = m_RC_Gmy_Class.RC_Fun_GetDtPar_3Point(ccdX, ccdY, axisX, axisY, circleX, circleY, ref Software_NinePoint.a_CalPar1.xxA, ref Software_NinePoint.a_CalPar1.yyA, ref Software_NinePoint.a_CalPar1.kX, ref Software_NinePoint.a_CalPar1.kY, ref Software_NinePoint.a_CalPar1.cX, ref Software_NinePoint.a_CalPar1.cY, ref Software_NinePoint.a_CalPar1.vRadius, ref errMsg);

            return tResult;

        }

        /// <summary>
        /// 根据相机拍照坐标，计算出参考固定组装位置的机构偏移量
        /// </summary>
        /// <param name="photoX">当前物料拍照坐标_X</param>
        /// <param name="photoY">当前物料拍照坐标_Y</param>
        /// <param name="photoR">当前物料拍照坐标_R</param>
        /// <param name="baseX">相机坐标系内，理想标准位置物料拍照坐标（亦即理想组装坐标，但是必须是相机坐标系内坐标）_X</param>
        /// <param name="baseY">相机坐标系内，理想标准位置物料拍照坐标（亦即理想组装坐标，但是必须是相机坐标系内坐标）_Y</param>
        /// <param name="baseR">相机坐标系内，理想标准位置物料拍照坐标（亦即理想组装坐标，但是必须是相机坐标系内坐标）_R</param>
        /// <param name="offsetX">返回结果，即机构从注册的标准组装位置，到正确组装位置需要叠加的偏移量，通过传址参数获取_X</param>
        /// <param name="offsetY">返回结果，即机构从注册的标准组装位置，到正确组装位置需要叠加的偏移量，通过传址参数获取_Y</param>
        /// <param name="offsetR">返回结果，即机构从注册的标准组装位置，到正确组装位置需要叠加的偏移量，通过传址参数获取_R</param>
        /// <remarks></remarks>
        public static bool m_RC_Gmy_CaluAxisOffset(double photoX, double photoY, double photoR, double baseX, double baseY, double baseR, ref double offsetX, ref double offsetY, ref double offsetR, ref string errMsg)
        {
            bool tResult = m_RC_Gmy_Class.RC_Fun_OffsetCal(photoX, photoY, photoR, baseX, baseY, baseR, ref offsetX, ref offsetY, ref offsetR, Software_NinePoint.hp_CalPar_L.xxA, Software_NinePoint.hp_CalPar_L.yyA, Software_NinePoint.hp_CalPar_L.kX, Software_NinePoint.hp_CalPar_L.kY, Software_NinePoint.hp_CalPar_L.cX, Software_NinePoint.hp_CalPar_L.cY, ref errMsg);

            return tResult;
        }
    }
}
