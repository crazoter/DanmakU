using UnityEngine;

namespace DanmakU.Fireables {

    public abstract class Fireable : IFireable {

        public IFireable Child { get; set; }

        public abstract void Fire(DanmakuConfig state);

        protected void Subfire(DanmakuConfig state) {
            //Debug.Log("Subfire");
            //Debug.Log("Child: "+Child.GetHashCode());
            if (Child == null)
                return;
            Child.Fire(state);
        }

    }

}