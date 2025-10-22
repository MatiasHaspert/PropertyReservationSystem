export default function Reviews({ reviews }) {
  if (!reviews || reviews.length === 0) return <p>No hay reseñas aún.</p>;

  return (
    <div className="mt-4">
      <h5>Reseñas:</h5>
      <ul className="list-unstyled">
        {reviews.map((r) => (
          <li key={r.id} className="mb-3 border-bottom pb-2">
            <strong>Rating:</strong> {r.rating} / 5 <br />
            <strong>Comentario:</strong> {r.comment} <br />
            <small>{new Date(r.date).toLocaleDateString()}</small>
          </li>
        ))}
      </ul>
    </div>
  );
}
