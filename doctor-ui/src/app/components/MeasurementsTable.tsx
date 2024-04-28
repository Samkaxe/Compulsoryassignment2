interface MeasurementsTable {
  items: {
    id: number;
    date: string;
    systolic: number;
    diastolic: number;
    patientSSN: string;
  }[];
}

const MeasurementsTable = ({ items }: MeasurementsTable) => {
  return (
    <div className="px-4 sm:px-6 lg:px-8 w-full">
      <div className="sm:flex sm:items-center">
        <div className="sm:flex-auto">
          <h1 className="text-base font-semibold leading-6 text-gray-900">
            Measurements
          </h1>
        </div>
        <div className="mt-4 sm:ml-16 sm:mt-0 sm:flex-none"></div>
      </div>
      <div className="mt-8 flow-root">
        <div className="-mx-4 -my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
          <div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
            <table className="min-w-full divide-y divide-gray-300">
              <thead>
                <tr>
                  <th
                    scope="col"
                    className="py-3.5 pl-4 pr-3 text-left text-sm font-semibold text-gray-900 sm:pl-0"
                  >
                    Date
                  </th>
                  <th
                    scope="col"
                    className="py-3.5 pl-4 pr-3 text-left text-sm font-semibold text-gray-900 sm:pl-0"
                  >
                    Systolic
                  </th>
                  <th
                    scope="col"
                    className="py-3.5 pl-4 pr-3 text-left text-sm font-semibold text-gray-900 sm:pl-0"
                  >
                    Diastolic
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-300">
                {items && items.map((item, index) => (
                  <tr key={index}>
                    <td className="py-3.5 pl-4 pr-3 text-sm text-gray-900 sm:pl-0">
                      {item.date}
                    </td>
                    <td className="py-3.5 pl-4 pr-3 text-sm text-gray-900 sm:pl-0">
                      {item.systolic}
                    </td>
                    <td className="py-3.5 pl-4 pr-3 text-sm text-gray-900 sm:pl-0">
                      {item.diastolic}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MeasurementsTable;
