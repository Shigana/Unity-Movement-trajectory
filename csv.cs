using UnityEngine;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class csv : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        onetimepos = transform.position;
        time1 = 0;
        GameObject.Find("CSVStop").GetComponent<Button>().interactable = false;
    }

    private Vector3 onetimepos;
    private float time1;
    public  bool saves;
    private double savetime;
    // Update is called once per frame
    void Update()
    {

        //Debug.Log(GameObject.Find("InputField").GetComponent<InputField>().text);

        if (saves)
        {

            if (savetime > 0.1)
            {
                onetimepos = transform.position;
                //StreamWriter sw = new StreamWriter(DataName, true, Encoding.GetEncoding("Shift_JIS"));
                StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/"  + DataName, true);　//If it is a stand-alone HMD, the path may not pass, so use the path of the persistent data directory.
                string[] onepos = { time1.ToString(), onetimepos.x.ToString(), onetimepos.y.ToString(), onetimepos.z.ToString() };
                string poslog = string.Join(",", onepos);
                sw.WriteLine(poslog);
                sw.Flush();
                sw.Close();
                //onetimepos = GameObject.Find("Cube").transform.position;
                //poslog ={ onetimepos.x.ToString() , onetimepos.y.ToString() , onetimepos.z.ToString() };
                //s2 = string.Join(",", poslog);
                savetime = 0;
            }
            time1 += Time.deltaTime;
            savetime += Time.deltaTime;
        }

    }
    private string DataName;
    byte filenum_1 = 0;
    byte filenum_10 = 0;
    byte filenum_100 = 0;

    public void RecStart()
    {
        if (!saves)
        {
            saves = true;
            GameObject.Find("CSVStart").GetComponent<Button>().interactable = false;
            GameObject.Find("CSVStop").GetComponent<Button>().interactable = true;

            DataName = "000歩行軌跡.csv";
            while (true)
            {
                if (File.Exists(Application.persistentDataPath + "/" + DataName)) //If it is a stand-alone HMD, the path may not pass, so use the path of the persistent data directory.
                {
                    filenum_1++;
                    if (filenum_1 > 9)
                    {
                        filenum_10++;
                        filenum_1 = 0;
                        if (filenum_10 > 9)
                        {
                            filenum_100++;
                            filenum_10 = 0;
                        }
                    }
                    DataName = "";
                    DataName += filenum_100;
                    DataName += filenum_10;
                    DataName += filenum_1;
                    DataName += "歩行軌跡.csv";
                }
                else
                {
                    break;
                }
            }
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + DataName, false);
            // ヘッダー出力
            string[] s1 = { "time", "x", "y", "z" };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);

            sw.Close();
        }
        else
        {
            saves = false;
            GameObject.Find("CSVStart").GetComponent<Button>().interactable = true;
            GameObject.Find("CSVStop").GetComponent<Button>().interactable = false;
        }
    }
}
