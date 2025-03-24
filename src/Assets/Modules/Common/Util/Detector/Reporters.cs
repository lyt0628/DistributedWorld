using UnityEngine;

namespace QS.Common.Util.Detector
{

    class CollisionEnterReporter : ReporterBehaviour<Collider>
    {


        private void OnCollisionEnter(Collision collision)
        {
            Add(collision.collider);
        }
    }

    class CollisionStayReporter : ReporterBehaviour<Collider>
    {


        private void OnCollisionStay(Collision collision)
        {
            Add(collision.collider);
        }
    }

    class CollisionExitReporter : ReporterBehaviour<Collider>
    {

        private void OnCollisionExit(Collision collision)
        {
            Add(collision.collider);
        }
    }

    class CollisionEnterAndStayReporter : ReporterBehaviour<Collider>
    {

        private void OnCollisionEnter(Collision collision)
        {
            Add(collision.collider);
        }

        private void OnCollisionStay(Collision collision)
        {
            Add(collision.collider);
        }
    }

    class CollisionEnterAndExitReporter : ReporterBehaviour<Collider>
    {


        private void OnCollisionEnter(Collision collision)
        {
            Add(collision.collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            Add(collision.collider);
        }
    }

    class CollisionStayAndExitReporter : ReporterBehaviour<Collider>
    {


        private void OnCollisionStay(Collision collision)
        {
            Add(collision.collider);
        }
        private void OnCollisionExit(Collision collision)
        {
            Add(collision.collider);
        }
    }

    class CollisionAllStageReporter : ReporterBehaviour<Collider>
    {


        private void OnCollisionEnter(Collision collision)
        {
            Add(collision.collider);
        }

        private void OnCollisionStay(Collision collision)
        {
            Add(collision.collider);
        }

        private void OnCollisionExit(Collision collision)
        {
            Add(collision.collider);
        }
    }

    class TriggerEnterReporter : ReporterBehaviour<Collider>
    {

        private void OnTriggerEnter(Collider other)
        {
            Add(other);
        }
    }

    class TriggerStayReporter : ReporterBehaviour<Collider>
    {
        private void OnTriggerStay(Collider other)
        {
            Add(other);
        }
    }

    class TriggerExitReporter : ReporterBehaviour<Collider>
    {
        private void OnTriggerExit(Collider other)
        {
            Add(other);
        }
    }

    class TriggerEnterAndStayReporter : ReporterBehaviour<Collider>
    {
        private void OnTriggerEnter(Collider other)
        {
            Add(other);
        }

        private void OnTriggerStay(Collider other)
        {
            Add(other);
        }
    }

    class TriggerEnterAndExitReporter : ReporterBehaviour<Collider>
    {
        private void OnTriggerEnter(Collider other)
        {
            Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Add(other);
        }
    }

    class TriggerStayAndExitReporter : ReporterBehaviour<Collider>
    {
        private void OnTriggerStay(Collider other)
        {
            Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Add(other);
        }
    }

    class TriggerAllStageReporter : ReporterBehaviour<Collider>
    {
        private void OnTriggerEnter(Collider other)
        {
            Add(other);
        }
        private void OnTriggerStay(Collider other)
        {
            Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Add(other);

        }

    }
}