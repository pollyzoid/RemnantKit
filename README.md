# Remnant 2 Research & Development

<sup><sub>Maybe Remnant: FTA too</sub></sup>

Currently in research stage for save files.

## Structure

- `docs/Saves/`
    - Save file reverse engineering resources, will be messy
- `src/RemnantKit`
    - Library sources
- `src/RemnantKit.Tools.UnSav`
    - CLI tool for decompressing Remnant 2 save files

## Goals

Mandatory disclaimer: this a project done on free time, progress will be spurious and development
may end at any point without prior notice.

- .NET library for accessing save file data (profile & world data)
- Simple UI for inspecting save file data without needing specialized tools
- Semi-automated extraction of item data (name, description, stats)
