# Changelog

## [1.2.3] - Development
### Changed
- Changed so that a new page can be created while keeping the previous page displayed.
  - The `Page` class is required to implement `IsKeepPreviousPage`.

## [1.2.2] - 2024-10-16
### Changed
- Dispose all pages when disposing of the manager.
- Manager destroys each page.

## [1.2.1] - 2024-07-22
### Added
- Added a generic button selection page.
- Added a Create() function with a prefab name as an argument to the PageManager.

## [1.2.0] - 2024-07-22
### Changed
- Changed so that the Manager closes the Page.

## [1.1.0] - 2024-07-22
### Changed
- Disruptive changes:
- Changed the PageBase class to the IPage interface.
- Changed to a format that calls the function of the Page instance instead of passing PageArgument.
- Changed so that PageManager generates pages using PageBuilder.
- Changed so that PageManager returns PageHandle.

## [1.0.1] - 2024-07-11
### Added
- Added samples.
- Added GetCount() and HasPage() into manager.
- Added PageChanged event into manager.

## [1.0.0] - 2024-07-10
### Added
- Initial assets
