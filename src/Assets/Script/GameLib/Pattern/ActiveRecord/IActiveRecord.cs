namespace GameLib.Pattern
{
    public interface IActiveRecord
    {
        /// var sword = Weapon.Create();
        /// sword.WeaponId = WeaponID.Sword;
        /// sword.Save();
        /// <summary>
        /// Update the record to data store.
        /// This method indentify a record by ID, which should not be null.
        /// If some attribute of record object is null, those data would not be updated.
        /// </summary>
        bool Update();

        /// <summary>
        /// Save all attribute of record to data store.
        /// This methid indentify a record by ID, which should not be null.
        /// All data will be updated, even null.
        /// </summary>
        bool Save();

        /// <summary>
        /// Delete the record from data store.
        /// </summary>
        bool Destroy();


        bool NewRecord { get; }
        bool Persisted { get; }

    }
}