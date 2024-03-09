namespace JAM.Audio
{
    [System.Serializable]
    public class SFXInterruptableDictionary : SFXStringDictionary
    {
        #region Private Methods
        
        private void InterruptAll()
        {
            foreach (var rack in this)
            {
                rack.Value.Instance.Stop();
            }
        }

        #endregion

        #region Public Methods

        public override void Play(string evt)
        {
            InterruptAll();
            base.Play(evt);
        }

        #endregion
    }
}
