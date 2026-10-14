import { useId } from 'react'

// A rating the player can ask the draw to lean toward: "Any" or one of the API's band names, rating 1 to 5 in order.
// A native select is keyboard and screen-reader friendly and stays one line at phone width.
export function RatingPicker({
  label,
  bands,
  value,
  disabled,
  onChange,
}: {
  label: string
  bands: string[]
  value: number | null
  disabled: boolean
  onChange: (rating: number | null) => void
}) {
  const id = useId()
  return (
    <div className="rating-picker">
      <label htmlFor={id}>{label}</label>
      <select
        id={id}
        value={value ?? ''}
        disabled={disabled}
        onChange={(event) => onChange(event.target.value === '' ? null : Number(event.target.value))}
      >
        <option value="">Any</option>
        {bands.map((band, index) => (
          <option key={band} value={index + 1}>
            {band}
          </option>
        ))}
      </select>
    </div>
  )
}
