# Sitecore Experience Commerce Plugins

- [x] Order number
- [x] Shipping countries
- [x] Different shipping method for different country
- [x] Delivery country condition
- [ ] New order status
  - [ ] WaitingForPayment - Default order status after order created. Move to Pending after payment confirmed, or move to ManuallyReview if payment need a manually review
  - [ ] ManuallyReview - Move order to this status if payment need a manually review. Next status could be: Pending, Cancelled
- [ ] A new API to received the payment notification and saved to the order
- [ ] Payment notification process minion
- [ ] Payment notification process pipeline
- [ ] 

## VS Extension

~~### Microsoft.OData.ConnectedService.vsix - 0.3.1~~

~~This is a preview version that works for me. The one that download from the VS market just not working.~~

For Sitecore XC 9.0+, you can use the latest version, and use the **update-proxy.ps1** script to update the generated file. The **update-proxy.ps1** file can be found at latest Sitecore Commerce SDK (Sitecore.Commerce.Engine.SDK.6.0.130.zip) or Tools folder in this repo.

