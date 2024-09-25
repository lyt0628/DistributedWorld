

namespace QS.API
{
    public enum EBuffStage
    {
        /*
         * Do not have any eddfect
         */
        None ,
        /*
         * Take Effect right now, and is one shot
         */
        IMMEDIATE,
        /*
         * Dynamicly compute Before Every Attack was generated
         */
        BEFORE_ATTACK,
        /*
         * Dynamicly compute After Every Attack was generated
         */

        AFTER_ATTACK,
        
        /*
         * Dynamic compute Before Every Injure 
         */
        
        BEFORE_INJURED,
        /*
         * Dynamicly compute after every Injure 
         */
        
        AFTER_INJURED,
        
        /*
         * Take effect after damage was put into cahracter
         */
        FINAL,

    }
}