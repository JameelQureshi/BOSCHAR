
using UnityEngine;
using UnityEngine.AI;
namespace Lean.Touch
{
    public class Select : MonoBehaviour
    {

        public Material hitMaterial;
        public Material hitMaterialOG;

        public GameObject bosch;
        public bool selected;
        public bool moving;
        public GameObject Options;

        public GameObject Option1;
        public GameObject Option2;

        // Use this for initialization
        private void Start()
        {
            this.GetComponent<NavMeshAgent>().enabled = true;
            this.GetComponent<Wander>().enabled = true;

            moving = true;
            //Option2.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        // Update is called once per frame
        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;


                if (Physics.Raycast(ray, out hitInfo))
                {

                    var rig = hitInfo.collider.GetComponent<Rigidbody>();
                    if (rig != null)
                    {
                        if (rig.tag == "Delete")
                        {

                            Destroy(rig.transform.root.gameObject);
                        }


                        // bosch.GetComponent<SkinnedMeshRenderer>().material = hitMaterial;
                        rig.GetComponent<LeanPinchScale>().enabled = true;
                        rig.GetComponent<LeanDragTranslate>().enabled = true;
                        rig.GetComponent<LeanTwistRotateAxis>().enabled = true;
                        rig.GetComponent<NavMeshAgent>().enabled = false;
                        rig.GetComponent<Wander>().enabled = false;
                        rig.gameObject.transform.GetChild(0).gameObject.SetActive(true);


                    }
                }
                else
                {



                    bosch.GetComponent<SkinnedMeshRenderer>().material = hitMaterialOG;
                    this.GetComponent<LeanPinchScale>().enabled = false;
                    this.GetComponent<LeanDragTranslate>().enabled = false;
                    this.GetComponent<LeanTwistRotateAxis>().enabled = false;
                    Options.SetActive(false);


                    if (moving == true)
                    {
                        this.GetComponent<NavMeshAgent>().enabled = true;
                        this.GetComponent<Wander>().enabled = true;
                    }

                    if (moving == false)
                    {
                        this.GetComponent<NavMeshAgent>().enabled = false;
                        this.GetComponent<Wander>().enabled = false;
                    }   






                }
            }

        }



    }
}