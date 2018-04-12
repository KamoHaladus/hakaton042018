namespace KnightFrank.Antares.Domain.Activity.QueryResults
{
    using System;
    using System.Collections.Generic;

    using KnightFrank.Antares.Common.Enums;
    using KnightFrank.Antares.Dal.Model;
    using KnightFrank.Antares.Dal.Model.Activity;
    using KnightFrank.Antares.Dal.Model.Common;
    using KnightFrank.Antares.Dal.Model.Portal;
    using KnightFrank.Antares.Dal.Model.Property;
    using KnightFrank.Antares.Domain.Appointment.QueryResults;
    using KnightFrank.Antares.Domain.Common;
    using KnightFrank.Antares.Domain.Common.QueryResults;
    using KnightFrank.Antares.Domain.Note.QueryResults;
    using KnightFrank.Antares.Domain.Referral.QueryResults;
    using KnightFrank.Antares.Domain.Tenancy.QueryResults;
    using KnightFrank.Antares.Domain.ViewModels;
    using KnightFrank.Antares.Domain.ViewModels.AssetCards;

    using Newtonsoft.Json;

    public class ActivityViewModel : IActivityWithPhoto, IProtectedResourceViewModel
    {
        public Guid Id { get; set; }

        public Guid AssetId { get; set; }

        public ActivityPropertyCardViewModel Property { get; set; }

        public ActivityDevelopmentCardViewModel Development { get; set; }

        public ActivityPhaseCardViewModel Phase { get; set; }

        public BuildingCardViewModel Building { get; set; }

        public ActivityPortfolioCardViewModel Portfolio { get; set; }

        [JsonIgnore]
        public MeasurementUnitValue TotalArea { get; set; }

        public AssetType AssetType { get; set; }

        public Guid PropertyTypeId { get; set; }

        public Guid ActivityStatusId { get; set; }

        public Guid ActivityTypeId { get; set; }

        public string ActivityTypeCode { get; set; }

        public List<PartyCardViewModel> Landlords { get; set; }

        public List<PartyCardViewModel> Vendors { get; set; }

        public List<PartyCardViewModel> Representatives { get; set; }

        public List<AgentViewModel> Agents { get; set; }

        public List<AgentViewModel> SubAgents { get; set; }

        public List<IntroducerViewModel> Introducers { get; set; }

        public decimal? AgreedInitialMarketingPrice { get; set; }

        public decimal? KfValuationPrice { get; set; }

        public decimal? VendorValuationPrice { get; set; }

        public NegotiatorViewModel LeadNegotiator { get; set; }

        public List<NegotiatorViewModel> SecondaryNegotiators { get; set; }

        public List<DepartmentWithFeeViewModel> Departments { get; set; }

        public PartyCardViewModel Solicitor { get; set; }

        public PartyCardViewModel LandlordAgent { get; set; }

        public PartyCardViewModel TenantAgent { get; set; }

        public PartyCardViewModel BillingContact { get; set; }

        public Guid? SourceId { get; set; }

        public string SourceDescription { get; set; }

        public Guid? SellingReasonId { get; set; }

        public string PitchingThreats { get; set; }

        public string KeyNumber { get; set; }

        public string AccessArrangements { get; set; }

        public Guid? KeyHolderTypeId { get; set; }

        public DepartmentCardViewModel KeyHolderDepartment { get; set; }

        public PartyCardViewModel KeyHolder { get; set; }

        public bool IsVacantProperty { get; set; }

        public string AlarmCodeIn { get; set; }

        public string AlarmCodeOut { get; set; }

        public decimal? ServiceChargeAmount { get; set; }

        public string ServiceChargeNote { get; set; }

        public decimal? GroundRentAmount { get; set; }

        public string GroundRentNote { get; set; }

        public string OtherCondition { get; set; }

        public Guid? DisposalTypeId { get; set; }

        public Guid? ConditionTypeId { get; set; }

        public decimal? ShortKfValuationWeeklyPrice { get; set; }

        public decimal? ShortKfValuationMonthlyPrice { get; set; }

        public decimal? LongKfValuationWeeklyPrice { get; set; }

        public decimal? LongKfValuationMonthlyPrice { get; set; }

        public bool ChainsExist { get; set; } = true;

        public Guid? PriceTypeId { get; set; }

        public decimal? ActivityPrice { get; set; }

        public decimal? ShortAskingWeekRent { get; set; }

        public decimal? ShortAskingMonthRent { get; set; }

        public decimal? LongAskingWeekRent { get; set; }

        public decimal? LongAskingMonthRent { get; set; }

        public bool AdvertisingPublishToWeb { get; set; }

        public string AdvertisingNote { get; set; }

        public bool SalesBoardUpToDate { get; set; }

        public DateTime? SalesBoardRemovalDate { get; set; }

        public string SalesBoardSpecialInstructions { get; set; }

        public Guid? AdvertisingPrPermittedTypeId { get; set; }

        public string AdvertisingPrContent { get; set; }

        public List<Portal> AdvertisingPortals { get; set; }

        public Guid? SalesBoardTypeId { get; set; }

        public Guid? SalesBoardStatusId { get; set; }

        public Guid AgencyTypeId { get; set; }

        public Guid? RefurbIndicatorId { get; set; }

        public DateTime? LastRefurbDate { get; set; }

        public Guid? GradeId { get; set; }

        public DateTime? MarketedDate { get; set; }

        public string Comments { get; set; }

        public decimal? EstimatedRates { get; set; }

        public decimal? EstimatedServiceCharge { get; set; }

        public decimal? QuotedSalePrice { get; set; }

        public MeasurementUnitValue QuotedCapitalValue { get; set; }

        public MeasurementUnitValue CalculatedCapitalValue { get; set; }

        public string QuotingTermsComments { get; set; }

        // ReSharper disable once InconsistentNaming
        public decimal? WAULT { get; set; }

        public decimal? Rent { get; set; }

        public decimal? Discount { get; set; }

        public decimal? NetInitialYield { get; set; }

        public DateTime? LeaseExpiryDate { get; set; }

        public string LeaseTerm { get; set; }

        public Guid? QuotingTypeId { get; set; }

        public Guid? QuotingRentTypeId { get; set; }

        public MeasurementUnitValue QuotingRent { get; set; }

        public decimal? QuotingRentPA { get; set; }

        public List<Guid> AreaIds { get; set; }

        public string FeeComment { get; set; }

        public Guid? FeeTypeId { get; set; }

        public Guid? FeeSplitTypeId { get; set; }

        public decimal? KnightFrankFeePercentage { get; set; }

        public decimal? KnightFrankFeeAmount { get; set; }

        public decimal? KnightFrankShareOfFee { get; set; }

        public DateTime? AgencyExpiryDate { get; set; }

        public string AgencyExpiryDateComment { get; set; }

        public decimal? FeePercentage { get; set; }

        public decimal? FeeValue { get; set; }

        public Guid? DurationTypeId { get; set; }

        public Guid? AgreementTypeId { get; set; }

        public Guid? InformationQualityId { get; set; }

        [JsonIgnore]
        public Guid? HeadLeaseId { get; set; }

        public TenancyCardModel HeadLease { get; set; }

        public bool Deleted { get; set; }

        public decimal? Yield { get; set; }

        public Guid? FurnishingId { get; set; }

        public bool? IsShariaCompliant { get; set; }

        public Guid? ConditionId { get; set; }

        public ActivitySummary Summary { get; set; }

        public MeasurementUnitValue AskingPricePerUnit { get; set; }

        public string SmallThumbnailUrl { get; set; }

        public string MediumThumbnailUrl { get; set; }

        public List<LeaseAdvisoryTenancyViewModel> LeaseAdvisoryTenancies { get; set; }

        public Guid? RepresentingId { get; set; }

        public DateTime? HeadsOfTermsDate { get; set; }

        public string FileReference { get; set; }

        public string TransactionExplanation { get; set; }

        public decimal? SurrenderPremium { get; set; }

        public Guid? PremiumPaidById { get; set; }

        public DateTime? SurrenderDate { get; set; }

        [JsonIgnore]
        public Guid? TenancyReviewId { get; set; }

        public TenancyReviewViewModel TenancyReview { get; set; }

        public Guid ServiceLineId { get; set; }

        public bool IsUploaded { get; set; }

        public string UniqueReferenceCode { get; set; }

        public decimal? ExpectedWeekRent { get; set; }

        public decimal? ExpectedMonthRent { get; set; }

        public DateTime? AnticipatedCompletionDate { get; set; }

        public bool PublishedToWeb { get; set; }

        public bool? IsSingleLet { get; set; }

        [JsonIgnore]
        public Guid? ReferralId { get; set; }

        public ReferralCardViewModel Referral { get; set; }

        public string AspasiaCubeReferenceId { get; set; }

        public bool AspasiaErrorOccurred { get; set; }

        public int Version { get; set; }

        public Guid CurrencyId { get; set; }

        public DateTime? CompletionDate { get; set; }

        public bool? HasSharedBathrooms { get; set; }

        public bool? HasSharedKitchens { get; set; }

        public string WebsiteUrl { get; set; }

        public string LastModifiedByUserName { get; set; }

        public DateTimeOffset LastWriteTime { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public NoteSimpleViewModel LatestNote { get; set; }

        public int NotesCount { get; set; }

        public List<AttributeValueViewModel> AttributeValues { get; set; }

        public DevelopmentAttributeValuesViewModel DevelopmentAttributeValues { get; set; }

        public List<CharacteristicViewModel> Characteristics { get; set; }

        public List<ActivityLocalisedViewModel> LocalisedValues { get; set; }

        public AppraisalMeetingViewModel AppraisalMeeting { get; set; }

        public VisibilityLevel VisibilityLevel { get; set; }

        public bool? PetAllowed { get; set; }

        public bool? AcceptsSharers { get; set; }

        public bool? AcceptsStudents { get; set; }

        public string PetSharersStudentsInfo { get; set; }

        public string ImportantInformation { get; set; }

        public ImageViewModel ActivityMainImage { get; set; }

        public decimal? ParkingPriceWeek { get; set; }

        public decimal? ParkingPriceMonth { get; set; }

        public DateTime? AvailableDate { get; set; }

        [JsonIgnore]
        public IAssetWithActivityImage AssetWithPhoto
        {
            get
            {
                switch (this.AssetType)
                {
                    case AssetType.Property: return this.Property;
                    case AssetType.Building: return this.Building;
                    case AssetType.Estate: return this.Estate;
                    case AssetType.Development: return this.Development;
                    case AssetType.Phase: return this.Phase?.Development;
                    default: return null;
                }
            }
        }

        public EstateCardViewModel Estate { get; set; }
    }
}