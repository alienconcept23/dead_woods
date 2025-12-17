# Cloud Save Strategy (notes)

- MVP: implement local save files in a known folder inside the user's documents folder (e.g., %USERPROFILE%\Documents\iso_survival_game\saves).
- Cloud sync (Phase 2): expose an option to let the user pick a cloud-synced folder (OneDrive, Google Drive, Dropbox). Documentation will include steps to link/choose OneDrive folder; later we can integrate APIs for server-side cloud saves.
- For Steam users, use Steam Cloud for cross-device sync as an option.
- Important: ensure save format is versioned and small; use encrypted JSON or binary with integrity checks to avoid corruption when syncing.
