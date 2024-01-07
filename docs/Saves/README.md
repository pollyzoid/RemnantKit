# Research & reverse engineering

## Tools

- [010 Editor](https://www.sweetscape.com/010editor/) for `.bt` templates (tiny `.1pj` project file
  included)

## Remnant 2 save files

Both profile and character saves share the same general structure. Remnant 2 has compressed save
files, which can be read after decompressing with zlib (after parsing the relevant parts from the
file). A small decompressing tool will be created soon.

010 templates have been slightly cleaned up – mostly naming- and structure-wise – but are very
lacking in documentation. Notes about the actual structure and different important bits will be
documented later.

Structure has been clean-room reversed, as the save file format is somewhat self-describing, save
for some guesswork with specific embedded structures where type information was not included. Some
types are based on very surface-level Unreal Engine knowledge, e.g. `FString/Text/Name` structures,
and named as such.

No application reversing has been done, as this is based on the Game Pass version, where Windows
very helpfully blocks read access to the executable. It could be bypassed, but it turned out to be
unnecessary for this.

### Profile saves

Shared between characters, contains most information one would assume save files to have, except any
actual world progress.

- Account-wide statistics
- Awards
- Character data
    - Appearance
    - Inventory
    - Traits
- etc.

### Character saves (/ world save)

Character-specific save file that contains information about the rolled campaign and adventure. This
can be used to figure out which worlds, quests, events and items have been rolled. Contains plenty
of more specific information, but priority is on actually useful bits.

### 010 Template

Complete structure-wise to my current knowledge. Successfully parses profile and character saves
from both small test files (new characters or little progress), and large ones (completed campaign
and in-progress adventure mode). 010 Editor will consume a decent amount of memory on large files,
on my machine a 20 MB character save took nearly 9 GB of memory.
