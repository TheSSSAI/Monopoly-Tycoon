# 1 Visual Foundation

## 1.1 Typography

### 1.1.1 Font Families

| Property | Value |
|----------|-------|
| Primary | 'Lora', serif |
| Secondary | 'Inter', sans-serif |
| Monospace | 'Roboto Mono', monospace |

### 1.1.2 Font Scale

#### 1.1.2.1 H1

| Property | Value |
|----------|-------|
| Size | 48px |
| Weight | 700 |
| Line Height | 1.2 |

#### 1.1.2.2 H2

| Property | Value |
|----------|-------|
| Size | 36px |
| Weight | 600 |
| Line Height | 1.3 |

#### 1.1.2.3 H3

| Property | Value |
|----------|-------|
| Size | 28px |
| Weight | 600 |
| Line Height | 1.4 |

#### 1.1.2.4 H4

| Property | Value |
|----------|-------|
| Size | 20px |
| Weight | 600 |
| Line Height | 1.5 |

#### 1.1.2.5 Body

| Property | Value |
|----------|-------|
| Size | 16px |
| Weight | 400 |
| Line Height | 1.6 |

#### 1.1.2.6 Caption

| Property | Value |
|----------|-------|
| Size | 14px |
| Weight | 400 |
| Line Height | 1.4 |

### 1.1.3.0 Responsive Scaling

Typography uses static sizes. The UI layout scales, but font sizes remain consistent across all supported desktop aspect ratios to ensure legibility.

## 1.2.0.0 Color Palette

### 1.2.1.0 Primary

| Property | Value |
|----------|-------|
| 50 | #E6F0F6 |
| 100 | #BEDAED |
| 200 | #94C3E3 |
| 300 | #6BAFDA |
| 400 | #429BD0 |
| 500 | #1987C7 |
| 600 | #126DA3 |
| 700 | #0C537F |
| 800 | #063A5B |
| 900 | #032037 |

### 1.2.2.0 Secondary

| Property | Value |
|----------|-------|
| 50 | #FBE9EC |
| 500 | #D7263D |
| 900 | #5A0A15 |

### 1.2.3.0 Neutrals

| Property | Value |
|----------|-------|
| White | #FFFFFF |
| Gray 50 | #F7F5F2 |
| Gray 100 | #E8E5E1 |
| Gray 500 | #7D7871 |
| Gray 900 | #1C1A17 |
| Black | #000000 |

### 1.2.4.0 Semantic

| Property | Value |
|----------|-------|
| Success | #1E8449 |
| Warning | #F39C12 |
| Error | #C0392B |
| Info | #2980B9 |

## 1.3.0.0 Spacing System

### 1.3.1.0 Scale

- 4px
- 8px
- 16px
- 24px
- 32px
- 48px
- 64px
- 96px

### 1.3.2.0 Grid System

A flexible layout system using anchored containers for the In-Game HUD and a centered, max-width container for modal and menu screens.

### 1.3.3.0 Container Widths

| Property | Value |
|----------|-------|
| Mobile | N/A |
| Tablet | N/A |
| Desktop | 1280px |

### 1.3.4.0 Responsive Spacing

Spacing values are static. Layout containers adapt to aspect ratios, but internal padding and margins remain consistent.

# 2.0.0.0 Component Specifications

## 2.1.0.0 Buttons

### 2.1.1.0 Primary

#### 2.1.1.1 Default State

| Property | Value |
|----------|-------|
| Background | #1987C7 |
| Text | #FFFFFF |
| Padding | 12px 24px |

#### 2.1.1.2 Hover State

| Property | Value |
|----------|-------|
| Background | #126DA3 |
| Text | #FFFFFF |
| Elevation | shadow-md |

#### 2.1.1.3 Active State

##### 2.1.1.3.1 Background

#0C537F

##### 2.1.1.3.2 Transform

scale(0.98)

#### 2.1.1.4.0 Disabled State

| Property | Value |
|----------|-------|
| Background | #94C3E3 |
| Text | #E6F0F6 |
| Cursor | not-allowed |

#### 2.1.1.5.0 Focus State

##### 2.1.1.5.1 Outline

2px solid #6BAFDA

##### 2.1.1.5.2 Outline Offset

2px

### 2.1.2.0.0 Secondary

#### 2.1.2.1.0 Default State

| Property | Value |
|----------|-------|
| Background | transparent |
| Text | #0C537F |
| Border | 1px solid #0C537F |
| Padding | 11px 23px |

#### 2.1.2.2.0 Hover State

##### 2.1.2.2.1 Background

#E6F0F6

##### 2.1.2.2.2 Text

#063A5B

#### 2.1.2.3.0 Active State

##### 2.1.2.3.1 Background

#BEDAED

##### 2.1.2.3.2 Transform

scale(0.98)

#### 2.1.2.4.0 Disabled State

| Property | Value |
|----------|-------|
| Background | transparent |
| Text | #94C3E3 |
| Border | 1px solid #94C3E3 |
| Cursor | not-allowed |

#### 2.1.2.5.0 Focus State

##### 2.1.2.5.1 Outline

2px solid #6BAFDA

##### 2.1.2.5.2 Outline Offset

2px

### 2.1.3.0.0 Accessibility

Buttons must have descriptive text or an aria-label. Keyboard focus must be clearly visible. All text/background combinations meet WCAG AA.

## 2.2.0.0.0 Form Inputs

### 2.2.1.0.0 Text Input

#### 2.2.1.1.0 Default State

| Property | Value |
|----------|-------|
| Border | 1px solid #7D7871 |
| Padding | 12px 16px |
| Background | #FFFFFF |
| Text | #1C1A17 |

#### 2.2.1.2.0 Focus State

##### 2.2.1.2.1 Border

2px solid #1987C7

##### 2.2.1.2.2 Box Shadow

0 0 0 3px rgba(25, 135, 199, 0.2)

#### 2.2.1.3.0 Error State

##### 2.2.1.3.1 Border

2px solid #C0392B

#### 2.2.1.4.0 Disabled State

##### 2.2.1.4.1 Background

#E8E5E1

##### 2.2.1.4.2 Cursor

not-allowed

#### 2.2.1.5.0 Accessibility

Every input must have a corresponding <label>. Error messages must be programmatically linked to the input using aria-describedby.

## 2.3.0.0.0 Navigation

### 2.3.1.0.0 Header

| Property | Value |
|----------|-------|
| Desktop Layout | N/A. Main menu uses a centered list of Primary But... |
| Mobile Layout | N/A |
| Active States | For menu lists, the active/hovered item will have ... |
| Accessibility | Focus must be managed when opening/closing menus. ... |

## 2.4.0.0.0 Data Display

### 2.4.1.0.0 Cards

#### 2.4.1.1.0 Default Card

| Property | Value |
|----------|-------|
| Padding | 24px |
| Border Radius | 8px |
| Shadow | 0 4px 6px rgba(0,0,0,0.1) |
| Background | #FFFFFF |

#### 2.4.1.2.0 Hover State

##### 2.4.1.2.1 Elevation

0 8px 12px rgba(0,0,0,0.15)

##### 2.4.1.2.2 Transform

translateY(-2px)

#### 2.4.1.3.0 Content Structure

Cards for Player HUDs, Property Details, and Save Slots will have a consistent structure with a header, content area, and optional footer for actions.

## 2.5.0.0.0 Feedback

### 2.5.1.0.0 Alerts

#### 2.5.1.1.0 Success

| Property | Value |
|----------|-------|
| Background | #E9F6ED |
| Icon | checkmark |
| Text | #1E8449 |

#### 2.5.1.2.0 Error

| Property | Value |
|----------|-------|
| Background | #FADBD8 |
| Icon | error |
| Text | #C0392B |

#### 2.5.1.3.0 Accessibility

Alerts and Toasts used for non-critical feedback must use ARIA live regions (aria-live='polite') to be announced by screen readers.

# 3.0.0.0.0 Interaction Patterns

## 3.1.0.0.0 Micro Interactions

### 3.1.1.0.0 Pattern Name

#### 3.1.1.1.0 Pattern Name

Button Press Feedback

#### 3.1.1.2.0 Trigger

User clicks/taps a button

#### 3.1.1.3.0 Animation

Scale down to 0.98 and change background color for 150ms using an ease-out curve.

#### 3.1.1.4.0 Accessibility

Animation is disabled if the user's system has 'prefers-reduced-motion' enabled.

### 3.1.2.0.0 Pattern Name

#### 3.1.2.1.0 Pattern Name

Modal Dialog Entry

#### 3.1.2.2.0 Trigger

A turn-halting decision is required

#### 3.1.2.3.0 Animation

A semi-transparent scrim fades in over 200ms. The modal dialog scales up from 0.95 to 1.0 and fades in over 250ms.

#### 3.1.2.4.0 Accessibility

Focus is programmatically moved to the first interactive element within the modal.

## 3.2.0.0.0 Loading States

- {'pattern_name': 'Initial Game Load', 'visual': 'A full-screen display with the game logo and a subtle, looping animation (e.g., a pulsing light or rotating gear).', 'duration': 'Displayed until the game state is fully loaded, max 10 seconds on recommended hardware.', 'accessibility': "A screen reader should announce 'Loading game, please wait.'"}

# 4.0.0.0.0 Implementation Guidelines

## 4.1.0.0.0 Css Architecture

### 4.1.1.0.0 Custom Properties

| Property | Value |
|----------|-------|
| Color Primary 500 | #1987C7 |
| Color Neutral 100 | #E8E5E1 |
| Spacing Md | 16px |
| Font Size Body | 16px |
| Font Family Primary | 'Lora', serif |

### 4.1.2.0.0 Naming Conventions

BEM (Block, Element, Modifier) is recommended for UI components. Example: .card__header--highlighted.

### 4.1.3.0.0 Component Organization

In Unity, create prefabs for each component. Scripts should be organized by feature (e.g., /Scripts/UI/MainMenu, /Scripts/UI/HUD).

### 4.1.4.0.0 Utility Classes

N/A for Unity implementation. Use public properties in component scripts to configure variants.

## 4.2.0.0.0 Responsive Strategy

### 4.2.1.0.0 Breakpoints

| Property | Value |
|----------|-------|
| Mobile | N/A |
| Tablet | N/A |
| Desktop | N/A |

### 4.2.2.0.0 Approach

Aspect Ratio Flexibility. UI is designed for a 16:9 baseline. Use Unity's Canvas Scaler with 'Scale With Screen Size' and a match mode of 0.5 (balanced). Anchor all HUD elements to screen corners. Center all menu and modal content.

### 4.2.3.0.0 Component Adaptations

Layout containers will expand/contract horizontally for 21:9 or vertically for 16:10, but component sizes remain consistent.

## 4.3.0.0.0 Accessibility Implementation

| Property | Value |
|----------|-------|
| Focus Management | A highly visible focus ring (2px solid #1987C7) mu... |
| Color Contrast | All text and UI control combinations must pass WCA... |
| Screen Reader Support | Not applicable for initial release as it's a Unity... |
| Keyboard Navigation | A logical tab order must be maintained for all scr... |

