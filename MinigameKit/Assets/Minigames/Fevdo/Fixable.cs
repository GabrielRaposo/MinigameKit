using UnityEngine;

namespace Fevdo{
    public class Fixable: MonoBehaviour{
        Transform fixables;
        void Reset(){
            GameObject fixFolder = GameObject.Find("Fixables");
            if(!fixFolder){
                fixFolder = new GameObject("Fixables");
                var go = GameObject.Instantiate(fixFolder,transform.root);
                go.name = "Fixables";
            }
            fixables = fixFolder.transform;
        }

        public void Fix(Transform obj){
            obj.parent = fixables;
        }
    }
}
