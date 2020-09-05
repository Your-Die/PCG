using Chinchillada.Foundation;
using UnityEngine;

namespace Mutiny.Thesis.Tools
{
    public class Highlighter : ChinchilladaBehaviour
    {
        [SerializeField] private Transform defaultParent;

        [SerializeField] private bool useStartingAsDefaultParent;

        public Transform CurrentTarget { get; private set; }

        public void Highlight(Transform target)
        {
            this.gameObject.SetActive(true);

            var transformCache = this.transform;

            transformCache.transform.SetParent(target);
            transformCache.localPosition = Vector3.zero;

            this.CurrentTarget = target;
        }

        public void Hide()
        {
            this.transform.SetParent(this.defaultParent);
            this.CurrentTarget = null;

            this.gameObject.SetActive(false);
        }

        protected override void Awake()
        {
            base.Awake();

            if (this.defaultParent == null && this.useStartingAsDefaultParent)
                this.defaultParent = this.transform.parent;

            this.Hide();
        }
    }
}