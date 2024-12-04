namespace QS.Api.Combat.Domain
{
    public enum BuffStages
    {
        /*
         * Do not have any eddfect
         */
        None,
        /*
         * Take Effect right now, and is one shot
         */
        Immeidate,
        /*
         * Dynamicly compute Before Every Attack was generated
         */
        Attack,

        /*
         * Dynamic compute Before Every Injure 
         */

        Injure,

        /*
         * Take effect after damage was put into cahracter
         */
        FINAL,

    }
}