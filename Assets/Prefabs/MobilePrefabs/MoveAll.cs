using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lean.Touch
{

    public class MoveAll : MonoBehaviour
    {
        public bool AllMove;

        public GameObject move1;
        public GameObject move2;


        public void Button1()
        {
            AllMove = !AllMove;
            if (AllMove == true)
            {
                move1.SetActive(true);
                move2.SetActive(false);
            

            }

            if (AllMove == false)
            {
                move2.SetActive(true);
                move1.SetActive(false);
              

            }
            {
                GameObject[] gos;
                gos = GameObject.FindGameObjectsWithTag("Bosch");


                foreach (GameObject go in gos)
                {
                    


                    if (AllMove == true) {
                       
                        go.GetComponent<Select>().moving = true;

                    }

                    if (AllMove == false)
                    {
                       
                        go.GetComponent<Select>().moving = false;

                    }

                }

            }
        }
    }
}
