namespace GrattaEVinci.Common.Model
{
    public record ResultTransaction
    {
        public decimal Amount { get; set; }
        public decimal AmountFromBonus { get; set; }
        public decimal AmountFromDeposit { get; set; }
        public decimal AmountFromWithdrawable { get; set; }
        public decimal Balance { get; set; }
        public decimal Bonus { get; set; }
       // public BonusCode TipoBonus { get; set; }
        public decimal WithdrawableAmount { get; set; }
    }
}
