public enum LastFmErrors {
	None = 0,
	ThisErrorDoesNotExist=1,
	InvalidServiceThisServiceDoesNotExist=2,
	/// <summary>
	/// No method with that name in this package
	/// </summary>
	InvalidMethod=3,
	/// <summary>
	/// You do not have permissions to access the service
	/// </summary>
	AuthenticationFailed=4,
	/// <summary>
	/// This service doesn't exist in that format
	/// </summary>
	InvalidFormat=5,
	/// <summary>
	/// Your request is missing a required parameter
	/// </summary>
	InvalidParameters=6,
	InvalidResourceSpecified=7,
	/// <summary>
	/// Most likely the backend service failed. Please try again.
	/// </summary>
	OperationFailed=8,
	/// <summary>
	/// Please re-authenticate
	/// </summary>
	InvalidSessionKey=9,
	/// <summary>
	/// You must be granted a valid key by last.fm
	/// </summary>
	InvalidAPIKey=10,
	/// <summary>
	/// This service is temporarily offline. Try again later.
	/// </summary>
	ServiceOffline=11,
	/// <summary>
	/// This station is only available to paid last.fm subscribers
	/// </summary>
	SubscribersOnly=12,
	InvalidMethodSignatureSupplied=13,
	/// <summary>
	/// This token has not been authorized
	/// </summary>
	UnauthorizedToken=14,
	ThisItemIsNotAvailableForStreaming=15,
	TheServiceIsTemporarilyUnavailablePleaseTryAgain=16,
	LoginUserRequiresToBeLoggedIn=17,
	/// <summary>
	/// This user has no free radio plays left. Subscription required.
	/// </summary>
	TrialExpired=18,
	/// <summary>
	/// There is not enough content to play this station
	/// </summary>
	NotEnoughContent=20,
	/// <summary>
	/// This group does not have enough members for radio
	/// </summary>
	NotEnoughMembers=21,
	/// <summary>
	/// This artist does not have enough fans for for radio
	/// </summary>
	NotEnoughFans=22,
	/// <summary>
	/// There are not enough neighbours for radio
	/// </summary>
	NotEnoughNeighbours=23,
	/// <summary>
	/// This user is not allowed to listen to radio during peak usage
	/// </summary>
	NoPeakRadio=24,
	/// <summary>
	/// Radio station not found
	/// </summary>
	RadioNotFound=25,
	/// <summary>
	/// This application is not allowed to make requests to the web services
	/// </summary>
	APIKeySuspended=26,
	/// <summary>
	/// This type of request is no longer supported
	/// </summary>
	Deprecated=27,
	/// <summary>
	/// Your IP has made too many requests in a short period, exceeding our API guidelines
	/// </summary>
	RateLimitExceded=29,
}
