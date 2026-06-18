# Diretrizes de Design — AutoImóvel

## 1. Tipografia

- **Fonte de Títulos & Impacto:** `Plus Jakarta Sans`, sans-serif (Google Fonts)
- **Fonte de Corpo & UI:** `Inter`, sans-serif (Google Fonts)

### Escala de Tamanhos e Pesos (Tailwind Classes)

| Uso | Classes |
|-----|---------|
| Super Títulos (saldos/patrimônio) | `font-['Plus_Jakarta_Sans']` `text-4xl` ou `text-5xl` `font-extrabold` `tracking-tight` |
| Títulos de Seção (cards/gráficos) | `font-['Plus_Jakarta_Sans']` `text-xl` ou `text-2xl` `font-bold` |
| Dados de Tabelas (placas/valores) | `text-sm` ou `text-base` `font-medium` |
| Legendas/Taxas | `text-xs` `font-normal` `text-slate-500` |

---

## 2. Paleta de Cores (Nova Identidade)

As cores da marca AutoImóvel são projetadas para transmitir confiança, modernidade e dinamismo. A paleta é otimizada para garantir acessibilidade e legibilidade em ambos os modos, claro e escuro.

### Cores Base da Marca (Tailwind & CSS Custom Properties)

Abaixo estão as cores base da marca, que servem como ponto de partida para a geração da paleta completa. Essas cores são mapeadas para o shade `600` em suas respectivas escalas, seguindo as convenções do Tailwind CSS.

| Nome | Hex (Base 600) | Uso Sugerido |
|------|----------------|--------------|
| **Deep Blue** | `#003F77` | Primária, Header Dark, Botões Primários, Fundo Dark |
| **Royal Blue** | `#005496` | Secundária, Hovers, Ícones, Elementos de Suporte |
| **Azure Blue** | `#007AA5` | Accent, Links, Status Ativo, Bordas |
| **Orange Accent** | `#FF6A14` | Destaques, CTAs de Impacto, Notificações, Gráficos |
| **Neutral Black** | `#000000` | Fundo Dark Principal, Texto em Light Mode (quando apropriado) |
| **Neutral White** | `#FFFFFF` | Fundo Light Principal, Texto em Dark Mode |
| **Highlight Light** | `#FF9E66` | Variação mais clara do Orange Accent para estados de hover. |

### Paleta Completa (Shades 50-950)

Para garantir flexibilidade e consistência, cada cor base possui uma gama completa de 11 tons, de 50 (muito claro) a 950 (muito escuro).

#### Primary (Deep Blue - `#003F77`)
| Shade | Hexadecimal | Uso Sugerido |
|-------|-------------|--------------|
| 50    | `#edf4f9`   | Elementos de fundo muito claros, divisores. |
| 100   | `#dae6f2`   | Fundos leves, bordas em Light Mode. |
| 200   | `#b3cbe0`   | Fundos de cards em Light Mode, seleções. |
| 300   | `#81a6c6`   | Elementos interativos em Light Mode. |
| 400   | `#4a7aa5`   | Texto secundário em Light Mode. |
| 500   | `#275884`   | Texto principal em Light Mode, ícones. |
| **600** | `#003F77`   | **Cor Principal da Marca**, botões primários, cabeçalhos. |
| 700   | `#10406b`   | Fundos escuros de cards, estados de hover. |
| 800   | `#072c4c`   | Fundo de elementos em Dark Mode, bordas em Dark Mode. |
| 900   | `#02192d`   | Fundo de elementos muito escuros em Dark Mode. |
| 950   | `#000d19`   | Fundo Dark principal. |

#### Secondary (Royal Blue - `#005496`)
| Shade | Hexadecimal | Uso Sugerido |
|-------|-------------|--------------|
| 50    | `#edf4f9`   | Fundos leves, divisores. |
| 100   | `#dae7f2`   | Bordas e elementos sutis. |
| 200   | `#b3cce0`   | Fundos secundários, destaque. |
| 300   | `#81a8c6`   | Ícones, detalhes. |
| 400   | `#4a7da5`   | Texto secundário. |
| 500   | `#275b84`   | Linhas de dados, texto de suporte. |
| **600** | `#005496`   | **Cor Secundária da Marca**, hovers, ícones. |
| 700   | `#10436b`   | Fundos de componentes em Dark Mode. |
| 800   | `#072e4c`   | Elementos mais escuros em Dark Mode. |
| 900   | `#021a2d`   | Bordas escuras, texto secundário em Dark Mode. |
| 950   | `#000e19`   | Fundo de elementos muito escuros em Dark Mode. |

#### Accent (Azure Blue - `#007AA5`)
| Shade | Hexadecimal | Uso Sugerido |
|-------|-------------|--------------|
| 50    | `#edf6f9`   | Backgrounds sutis, detalhes de borda. |
| 100   | `#daebf2`   | Destaques em light mode. |
| 200   | `#b3d4e0`   | Fundos de notificação, tags. |
| 300   | `#81b4c6`   | Destaques, links. |
| 400   | `#4a8da5`   | Ícones, indicadores de status. |
| 500   | `#276c84`   | Texto de destaque, barras de progresso. |
| **600** | `#007AA5`   | **Cor de Destaque da Marca**, links ativos, status. |
| 700   | `#10536b`   | Ícones em Dark Mode. |
| 800   | `#073a4c`   | Bordas em Dark Mode. |
| 900   | `#02222d`   | Texto de destaque em Dark Mode. |
| 950   | `#001219`   | Elementos de UI em Dark Mode. |

#### Highlight (Orange Accent - `#FF6A14`)
| Shade | Hexadecimal | Uso Sugerido |
|-------|-------------|--------------|
| 50    | `#f9f1ed`   | Fundos de alerta, tags. |
| 100   | `#f2e2da`   | Destaques muito claros. |
| 200   | `#e0c3b3`   | Fundos de notificação. |
| 300   | `#c69a81`   | Destaques de texto. |
| 400   | `#a56b4a`   | Elementos de alerta. |
| 500   | `#844927`   | Botões secundários. |
| **600** | `#FF6A14`   | **Cor de Destaque Forte**, CTAs, mensagens de erro/sucesso. |
| 700   | `#6b3110`   | Hovers de botões de destaque. |
| 800   | `#4c2007`   | Ícones de alerta em Dark Mode. |
| 900   | `#2d1202`   | Texto de erro em Dark Mode. |
| 950   | `#190900`   | Elementos muito escuros de destaque em Dark Mode. |

#### Neutrais
| Nome | Hexadecimal | Uso Sugerido (Light/Dark Mode) |
|------|-------------|---------------------------------|
| **Black** | `#000000`   | Fundo principal Dark, texto em Light Mode, elementos sólidos. |
| **White** | `#FFFFFF`   | Fundo principal Light, texto em Dark Mode, elementos claros. |
| **HighlightLight** | `#FF9E66`   | Variação clara do Orange Accent, hovers. |

### Estratégia de Modos (Tailwind & Custom Properties)

A aplicação suporta modos claro e escuro, alternando a classe `dark` no elemento `<html>` e usando as variantes `dark:` do Tailwind, juntamente com as variáveis CSS customizadas.

#### Light Mode
- **Background Principal:** `--color-bg: #FFFFFF;` (`bg-white`)
- **Card Background:** `--color-surface: #f8fafc;` (`bg-brand-primary-50` ou `bg-brand-primary-100`)
- **Texto Principal:** `--color-text: #003F77;` (`text-brand-primary-700` ou `text-brand-primary-900`)
- **Texto Secundário:** `--color-text-muted: #005496;` (`text-brand-secondary-500` ou `text-brand-accent-400`)
- **Bordas:** `--color-border: #dae6f2;` (`border-brand-primary-100`)

#### Dark Mode
- **Background Principal:** `--color-bg: #000000;` (`bg-black`)
- **Card Background:** `--color-surface: #02192d;` (`bg-brand-primary-900`)
- **Texto Principal:** `--color-text: #FFFFFF;` (`text-white` ou `text-brand-primary-50`)
- **Texto Secundário:** `--color-text-muted: #81a6c6;` (`text-brand-primary-300` ou `text-brand-accent-200`)
- **Bordas:** `--color-border: #072c4c;` (`border-brand-primary-800`)

---

## 3. Layout e Espaçamento

- **Container principal:** `max-w-7xl mx-auto px-4 sm:px-6 lg:px-8`
- **Padding interno de cards:** `p-6`
- **Gap entre cards:** `gap-4 md:gap-6`
- **Grid padrão:** `grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4`
- **Borda arredondada:** `rounded-2xl` para cards, `rounded-xl` para inputs/botões
- **Sombra:** `shadow-lg` no light mode (usando `rgba(0, 63, 119, 0.15)`), `shadow-none` no dark mode

---

## 4. Componentes Compartilhados (RCL — AutoImovel.UI.Shared)

### DataTable
- Header: `bg-brand-primary-100`, texto `text-brand-primary-600` `font-bold`
- Linhas: `hover:bg-brand-primary-50`
- Status badges: `px-3 py-1 text-[10px] font-black uppercase rounded-full`

### StatCard
- Label: `text-xs uppercase font-bold tracking-widest text-brand-accent-600`
- Valor: `font-['Plus_Jakarta_Sans']` `text-2xl font-black`
- Accent: Usar `text-brand-highlight-600` (Laranja) para valores de destaque.

---

## 5. Formulários

- Inputs: `border-brand-primary-100 dark:border-brand-primary-800 rounded-xl bg-white dark:bg-black/50 focus:ring-brand-accent-600`
- Botão Primário: `bg-brand-primary-600 hover:bg-brand-secondary-600 text-white`
- Botão CTA (Destaque): `bg-brand-highlight-600 hover:opacity-90 text-white font-bold`

---

## 6. Gráficos (Chart.js)

- Cores: `brand-primary-600`, `brand-accent-600`, `brand-highlight-600`.
- Grid lines: `rgba(0, 63, 119, 0.05)` (Light) / `rgba(255, 255, 255, 0.05)` (Dark).

---

## 7. Animações

- Transições de página: `animate-fade-in` (opacity 0 → 1, 300ms)
- Hover em cards: `transition-colors duration-200`
- Hover em linhas de tabela: `transition-colors`
- Spinner de loading: `animate-spin` com ícone `svg`

---

## 8. Modos (Dark/Light)

- Suporte a ambos os modos via classe `dark`.
- Transição suave entre temas.

---

## 9. Responsividade

- Mobile-first com breakpoints Tailwind.
- Sidebar colapsa para `fixed` overlay em `< lg`.
- Grid de cards: 1 coluna mobile, 2 tablet, 3+ desktop.

---

## 3. Layout e Espaçamento

- **Container principal:** `max-w-7xl mx-auto px-4 sm:px-6 lg:px-8`
- **Padding interno de cards:** `p-6`
- **Gap entre cards:** `gap-4 md:gap-6`
- **Grid padrão:** `grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4`
- **Borda arredondada:** `rounded-2xl` para cards, `rounded-xl` para inputs/botões
- **Sombra:** `shadow-lg` no light mode, `shadow-none` no dark mode

---

## 4. Componentes Compartilhados (RCL — AutoImovel.UI.Shared)

### DataTable
- Header: `bg-brand-primary/10`, texto `text-brand-primary` `font-bold`
- Linhas: `hover:bg-brand-primary/5`
- Status badges: `px-3 py-1 text-[10px] font-black uppercase rounded-full`

### StatCard
- Label: `text-xs uppercase font-bold tracking-widest text-brand-accent`
- Valor: `font-['Plus_Jakarta_Sans']` `text-2xl font-black`
- Accent: Usar `text-brand-highlight` (Laranja) para valores de destaque.

---

## 5. Formulários

- Inputs: `border-slate-200 dark:border-brand-primary/30 rounded-xl bg-white dark:bg-black/50 focus:ring-brand-accent`
- Botão Primário: `bg-brand-primary hover:bg-brand-secondary text-white`
- Botão CTA (Destaque): `bg-brand-highlight hover:opacity-90 text-white font-bold`

---

## 6. Gráficos (Chart.js)

- Cores: `Deep Blue`, `Azure`, `Orange Accent`.
- Grid lines: `rgba(0,0,0,0.05)` (Light) / `rgba(255,255,255,0.05)` (Dark).

---

## 7. Animações

- Transições de página: `animate-fade-in` (opacity 0 → 1, 300ms)
- Hover em cards: `transition-colors duration-200`
- Hover em linhas de tabela: `transition-colors`
- Spinner de loading: `animate-spin` com ícone `svg`

---

## 8. Modos (Dark/Light)

- Suporte a ambos os modos via classe `dark`.
- Transição suave entre temas.

---

## 9. Responsividade

- Mobile-first com breakpoints Tailwind.
- Sidebar colapsa para `fixed` overlay em `< lg`.
- Grid de cards: 1 coluna mobile, 2 tablet, 3+ desktop.
